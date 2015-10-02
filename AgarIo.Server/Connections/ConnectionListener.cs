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

        private static readonly object ConnectionsLock = new object();

        private readonly IConnectionSettings _connectionSettings;

        private readonly IConnectionFactory _connectionFactory;

        private readonly IPlayerRepository _playerRepository;

        private readonly IAdminCredentials _adminCredentials;

        private readonly List<IConnection> _connections;

        private readonly TaskFactory _taskFactory;

        public ConnectionListener(
            IConnectionSettings connectionSettings,
            IConnectionFactory connectionFactory,
            IPlayerRepository playerRepository,
            IAdminCredentials adminCredentials)
        {
            _connectionSettings = connectionSettings;
            _connectionFactory = connectionFactory;
            _playerRepository = playerRepository;
            _adminCredentials = adminCredentials;

            _connections = new List<IConnection>();

            var taskScheduler = new ThreadPerTaskScheduler();
            _taskFactory = new TaskFactory(taskScheduler);
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var listener = new TcpListener(IPAddress.Any, _connectionSettings.Port);
            listener.Server.NoDelay = true;
            listener.Server.ReceiveTimeout = 2000;
            listener.Server.SendTimeout = 2000;
            listener.Start();

            Log.Info($"Listening on *:{_connectionSettings.Port}");

            await HandleConnectionsAsync(listener, cancellationToken).ConfigureAwait(false);
        }

        public void Update()
        {
            lock (ConnectionsLock)
            {
                foreach (var connection in _connections)
                {
                    connection.Update();
                }
            }
        }

        private async Task HandleConnectionsAsync(TcpListener listener, CancellationToken cancellationToken)
        {
            var clientTasks = new List<Task>();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var tcpClient = await listener.AcceptTcpClientAsync().ContinueWith(t => t.Result, cancellationToken).ConfigureAwait(false);
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
                writer.BaseStream.WriteTimeout = 2000;
                writer.AutoFlush = true;
                using (var reader = new StreamReader(tcpClient.GetStream()))
                {
                    reader.BaseStream.ReadTimeout = 2000;
                    var handleConnectionTask = HandleConnection(cancellationTokenSource, reader, writer);
                    await _taskFactory.StartNew(() => handleConnectionTask, cancellationToken).Unwrap();
                }
            }
        }

        private async Task HandleConnection(
            CancellationTokenSource cancellationTokenSource,
            StreamReader reader,
            StreamWriter writer)
        {
            var json = await reader.ReadJsonAsync(cancellationTokenSource.Token);

            var loginDto = json.FromJson<LoginDto>();

            var commandResponseDto = new CommandResponseDto { ErrorCode = (int)CommandErrorCode.Success };
            if (!TryAuthorize(loginDto))
            {
                commandResponseDto.ErrorCode = (int)CommandErrorCode.WrongLogin;
                await writer.WriteLineAsync(commandResponseDto.ToJson()).ConfigureAwait(false);
                return;
            }

            await writer.WriteLineAsync(commandResponseDto.ToJson()).ConfigureAwait(false);

            var connection = _connectionFactory.Create(loginDto);
            lock (ConnectionsLock)
            {
                _connections.Add(connection);
            }
            try
            {
                await connection.RunAsync(reader, writer, cancellationTokenSource).ConfigureAwait(false);
            }
            finally
            {
                lock (ConnectionsLock)
                {
                    _connections.Remove(connection);
                }

                Log.Info($"{loginDto.Login} disconnected");
            }
        }

        private bool TryAuthorize(LoginDto loginDto)
        {
            if (loginDto.IsAdmin)
            {
                return loginDto.Login == _adminCredentials.AdminLogin && loginDto.Password == _adminCredentials.AdminPassword;
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