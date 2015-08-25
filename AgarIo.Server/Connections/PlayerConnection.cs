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

        private readonly BufferBlock<string> _dataToSend;

        private readonly LoginDto _loginDto;

        private Player _player;

        public PlayerConnection(LoginDto loginDto, IPlayerCommandFactory playerCommandFactory, IPlayerRepository playerRepository)
        {
            _playerCommandFactory = playerCommandFactory;
            _playerRepository = playerRepository;

            _loginDto = loginDto;

            _dataToSend = new BufferBlock<string>();
        }

        public async Task RunAsync(
            IGame game,
            TextReader reader,
            TextWriter writer,
            CancellationTokenSource cancellationTokenSource)
        {
            _player = _playerRepository.Register(_loginDto.Login, _loginDto.Password);
            var handleIncomingDataTask = HandleIncomingDataAsync(reader, game, cancellationTokenSource.Token);
            var handleOutgoingDataTask = HandleOutgoingDataAsync(writer, cancellationTokenSource.Token);
            await Task.WhenAny(handleIncomingDataTask, handleOutgoingDataTask);
            cancellationTokenSource.Cancel();
            await Task.WhenAll(handleIncomingDataTask, handleOutgoingDataTask);
            _playerRepository.Unregister(_player);
        }

        private async Task HandleIncomingDataAsync(TextReader reader, IGame game, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var command = await _playerCommandFactory.CreateAsync(reader, cancellationToken);
                    if (command == null)
                    {
                        continue;
                    }

                    command.Validate(_player, game);

                    var commandResponseDto = command.Execute(_player, game);
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