namespace AgarIo.Server.Connections
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    using AgarIo.Contract;
    using AgarIo.Contract.AdminCommands;
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.AdminCommands;
    using AgarIo.Server.CommandExceptions;
    using AgarIo.Server.Logic;
    using AgarIo.SystemExtension;

    using AutoMapper;

    public class AdminConnection : IConnection
    {
        private enum PushDataState
        {
            None,

            First,

            Continue
        }

        private readonly IAdminCommandFactory _adminCommandFactory;

        private readonly IStateTracker _stateTracker;

        private readonly IGame _game;

        private readonly BufferBlock<string> _dataToSend;

        private PushDataState _pushDataState;

        public AdminConnection(IAdminCommandFactory adminCommandFactory, IStateTracker stateTracker, IGame game)
        {
            _adminCommandFactory = adminCommandFactory;
            _stateTracker = stateTracker;
            _game = game;

            _dataToSend = new BufferBlock<string>();

            _pushDataState = PushDataState.None;
        }

        public async Task RunAsync(TextReader reader, TextWriter writer, CancellationTokenSource cancellationTokenSource)
        {
            var handleIncomingDataTask = HandleIncomingDataAsync(reader, cancellationTokenSource.Token);
            var handleOutgoingDataTask = HandleOutgoingDataAsync(writer, cancellationTokenSource.Token);
            await Task.WhenAny(handleIncomingDataTask, handleOutgoingDataTask);
            cancellationTokenSource.Cancel();
            await Task.WhenAll(handleIncomingDataTask, handleOutgoingDataTask);
        }

        public void Update()
        {
            var statePushDto = new StatePushDto();

            switch (_pushDataState)
            {
                case PushDataState.None:
                    return;
                case PushDataState.First:
                    statePushDto.AddedBlobs = Mapper.Map<BlobDto[]>(_game.Blobs);
                    statePushDto.RemovedBlobs = new BlobDto[] { };
                    statePushDto.UpdatedBlobs = new BlobDto[] { };
                    _pushDataState = PushDataState.Continue;
                    break;
                case PushDataState.Continue:
                    statePushDto.AddedBlobs = Mapper.Map<BlobDto[]>(_stateTracker.AddedBlobs);
                    statePushDto.RemovedBlobs = Mapper.Map<BlobDto[]>(_stateTracker.RemovedBlobs);
                    statePushDto.UpdatedBlobs = Mapper.Map<BlobDto[]>(_stateTracker.UpdatedBlobs);
                    break;
            }

            statePushDto.WorldSize = _game.Size;
            statePushDto.GameModeType = GameModeType.Classic;
            statePushDto.CustomGameModeData = _game.GameMode.GetCustomData().ToJson();

            Send(statePushDto);
        }

        private async Task HandleIncomingDataAsync(TextReader reader, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var command = await _adminCommandFactory.CreateAsync(reader, cancellationToken);
                    if (command == null)
                    {
                        continue;
                    }

                    command.Validate(_game);

                    var commandResponseDto = command.Execute(_game);
                    Send(commandResponseDto);

                    if (command is StartPushingStateAdminCommand)
                    {
                        _pushDataState = PushDataState.First;
                    }
                }
                catch (CommandException exception)
                {
                    var commandResponseDto = new CommandResponseDto
                    {
                        ErrorCode = (int)exception.ErrorCode,
                        Message = exception.Message
                    };
                    Send(commandResponseDto);
                }
            }
        }

        private async Task HandleOutgoingDataAsync(TextWriter writer, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var data = await _dataToSend.ReceiveAsync(cancellationToken);

                await writer.WriteLineAsync(data).WithCancellation(cancellationToken);
            }
        }

        private void Send(object data)
        {
            _dataToSend.Post(data.ToJson());
        }
    }
}