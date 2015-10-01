namespace AgarIo.Server.AdminCommands
{
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.Logic;
    using AgarIo.Server.Logic.GameModes;
    using AgarIo.Server.Logic.Physics;

    public class StartGameAdminCommand : AdminCommand
    {
        private readonly int _size;

        private readonly IPhysics _physics;

        private readonly IStateTracker _stateTracker;

        private readonly IPlayerRepository _playerRepository;

        public StartGameAdminCommand(int size, IPhysics physics, IStateTracker stateTracker, IPlayerRepository playerRepository)
        {
            _size = size;
            _physics = physics;
            _stateTracker = stateTracker;
            _playerRepository = playerRepository;
        }

        public override void Validate(IGame game)
        {
        }

        public override CommandResponseDto Execute(IGame game)
        {
            game.Stop();
            game.Start(_size, new ClassicGameMode(game, _physics, _stateTracker, _playerRepository));

            return Success;
        }
    }
}