namespace AgarIo.Server.Connections
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    using AgarIo.Contract;
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.CommandExceptions;
    using AgarIo.Server.Logic;
    using AgarIo.Server.PlayerCommands;
    using AgarIo.SystemExtension;

    public class PlayerConnection : IConnection
    {
        private readonly IPlayerCommandFactory _playerCommandFactory;

        private readonly IPlayerRepository _playerRepository;

        private readonly IGame _game;

        private readonly BufferBlock<string> _dataToSend;

        private readonly LoginDto _loginDto;

        private Player _player;

        public PlayerConnection(LoginDto loginDto, IPlayerCommandFactory playerCommandFactory, IPlayerRepository playerRepository, IGame game)
        {
            _playerCommandFactory = playerCommandFactory;
            _playerRepository = playerRepository;
            _game = game;

            _loginDto = loginDto;

            _dataToSend = new BufferBlock<string>();
        }

        public async Task RunAsync(
            StreamReader reader,
            StreamWriter writer,
            CancellationTokenSource cancellationTokenSource)
        {
            _player = _playerRepository.Register(_loginDto.Login, _loginDto.Password);
            try
            {
                var handleIncomingDataTask = HandleIncomingDataAsync(reader, cancellationTokenSource.Token);
                var handleOutgoingDataTask = HandleOutgoingDataAsync(writer, cancellationTokenSource.Token);
                await Task.WhenAny(handleIncomingDataTask, handleOutgoingDataTask).ConfigureAwait(false);
                cancellationTokenSource.Cancel();
                await Task.WhenAll(handleIncomingDataTask, handleOutgoingDataTask).ConfigureAwait(false);
            }
            finally
            {
                _playerRepository.Unregister(_player);
            }
        }

        public void Update()
        {
        }

        private async Task HandleIncomingDataAsync(StreamReader reader, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var command = await _playerCommandFactory.CreateAsync(reader, cancellationToken).ConfigureAwait(false);
                    if (command == null)
                    {
                        if (reader.EndOfStream)
                        {
                            return;
                        }

                        continue;
                    }

                    command.Validate(_player, _game);

                    var commandResponseDto = command.Execute(_player, _game);
                    Send(commandResponseDto);
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
                catch
                {
                    return;
                }
            }
        }

        private async Task HandleOutgoingDataAsync(StreamWriter writer, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var data = await _dataToSend.ReceiveAsync(cancellationToken).ConfigureAwait(false);

                    await writer.WriteLineAsync(data).WithCancellation(cancellationToken).ConfigureAwait(false);
                }
                catch
                {
                    return;
                }
            }
        }

        private void Send(object data)
        {
            _dataToSend.Post(data.ToJson());
        }
    }
}