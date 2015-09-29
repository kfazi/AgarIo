namespace AgarIo.Server.Connections
{
    using AgarIo.Contract;
    using AgarIo.Server.AdminCommands;
    using AgarIo.Server.Logic;
    using AgarIo.Server.PlayerCommands;

    using NLog;

    internal class ConnectionFactory : IConnectionFactory
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly IAdminCommandFactory _adminCommandFactory;

        private readonly IPlayerCommandFactory _playerCommandFactory;

        private readonly IPlayerRepository _playerRepository;

        private readonly IStateTracker _stateTracker;

        private readonly IGame _game;

        public ConnectionFactory(
            IAdminCommandFactory adminCommandFactory,
            IPlayerCommandFactory playerCommandFactory,
            IPlayerRepository playerRepository,
            IStateTracker stateTracker,
            IGame game)
        {
            _adminCommandFactory = adminCommandFactory;
            _playerCommandFactory = playerCommandFactory;
            _playerRepository = playerRepository;
            _stateTracker = stateTracker;
            _game = game;
        }

        public IConnection Create(LoginDto loginDto)
        {
            if (loginDto.IsAdmin)
            {
                Log.Info($"Admin {loginDto.Login} logged in");
                return new AdminConnection(_adminCommandFactory, _stateTracker, _game);
            }

            Log.Info($"Player {loginDto.Login} logged in");
            return new PlayerConnection(loginDto, _playerCommandFactory, _playerRepository, _game);
        }
    }
}