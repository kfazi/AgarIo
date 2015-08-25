namespace AgarIo.Server.Connections
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;

    using AgarIo.Contract;
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.CommandExceptions;
    using AgarIo.Server.Infrastructure;
    using AgarIo.Server.Logic;
    using AgarIo.SystemExtension;

    using NLog;

    public class ConnectionListener : IConnectionListener
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly IConnectionSettings _connectionSettings;

        private readonly IConnectionFactory _connectionFactory;

        private readonly IPlayerRepository _playerRepository;

        private readonly IGame _game;

        public ConnectionListener(
            IGame game,
            IConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory,
            IPlayerRepository playerRepository)
        {
            _game = game;
            _connectionSettings = connectionSettings;
            _connectionFactory = connectionFactory;
            _playerRepository = playerRepository;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var listener = new TcpListener(IPAddress.Any, _connectionSettings.Port);
            listener.Start();

            Log.Info($"Listening on *:{_connectionSettings.Port}");

            await HandleConnectionsAsync(listener, cancellationToken);
        }

        private async Task HandleConnectionsAsync(TcpListener listener, CancellationToken cancellationToken)
        {
            var clientTasks = new List<Task>();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var tcpClient = await listener.AcceptTcpClientAsync().ContinueWith(t => t.Result, cancellationToken);
                    clientTasks.RemoveAll(x => x.IsCompleted);
                    clientTasks.Add(HandleConnection(tcpClient, cancellationToken));
                }
            }
            finally
            {
                await Task.WhenAll(clientTasks);
            }
        }

        private async Task HandleConnection(TcpClient tcpClient, CancellationToken cancellationToken)
        {
            var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            using (var writer = new StreamWriter(tcpClient.GetStream()))
            {
                writer.AutoFlush = true;
                using (var reader = new StreamReader(tcpClient.GetStream()))
                {
                    await HandleConnection(cancellationTokenSource, reader, writer);
                }
            }
        }

        private async Task HandleConnection(
            CancellationTokenSource cancellationTokenSource,
            TextReader reader,
            TextWriter writer)
        {
            var json = await reader.ReadJsonAsync(cancellationTokenSource.Token);

            var loginDto = json.FromJson<LoginDto>();

            var commandResponseDto = new CommandResponseDto { ErrorCode = (int)CommandErrorCode.Success };
            if (!TryAuthorize(loginDto))
            {
                commandResponseDto.ErrorCode = (int)CommandErrorCode.WrongLogin;
            }

            await writer.WriteLineAsync(commandResponseDto.ToJson());

            var connection = _connectionFactory.Create(loginDto);
            await connection.RunAsync(_game, reader, writer, cancellationTokenSource);
        }

        private bool TryAuthorize(LoginDto loginDto)
        {
            if (loginDto.IsAdmin)
            {
                return true;
            }

            var player = _playerRepository.Players.FirstOrDefault(x => x.Name == loginDto.Login);
            if (player == null)
            {
                return true;
            }

            return player.Password == loginDto.Password;
        }
    }
}