namespace AgarIo.Server.Connections
{
    using AgarIo.Contract;
    using AgarIo.Server.AdminCommands;
    using AgarIo.Server.PlayerCommands;

    internal class ConnectionFactory : IConnectionFactory
    {
        private readonly IAdminCommandFactory _adminCommandFactory;

        private readonly IPlayerCommandFactory _playerCommandFactory;

        private readonly IPlayerRepository _playerRepository;

        public ConnectionFactory(
            IAdminCommandFactory adminCommandFactory,
            IPlayerCommandFactory playerCommandFactory,
            IPlayerRepository playerRepository)
        {
            _adminCommandFactory = adminCommandFactory;
            _playerCommandFactory = playerCommandFactory;
            _playerRepository = playerRepository;
        }

        public IConnection Create(LoginDto loginDto)
        {
            if (loginDto.IsAdmin)
            {
                return new AdminConnection(_adminCommandFactory);
            }

            return new PlayerConnection(loginDto, _playerCommandFactory, _playerRepository);
        }
    }
}