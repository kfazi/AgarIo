namespace AgarIo.Server.AdminCommands
{
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.Logic;
    using AgarIo.Server.Logic.GameModes;
    using AgarIo.Server.Logic.Physics;

    public class DefineWorldAdminCommand : AdminCommand
    {
        private readonly int _size;

        private readonly IPhysics _physics;

        private readonly IStateTracker _stateTracker;

        public DefineWorldAdminCommand(int size, IPhysics physics, IStateTracker stateTracker)
        {
            _size = size;
            _physics = physics;
            _stateTracker = stateTracker;
        }

        public override void Validate(IGame game)
        {
        }

        public override CommandResponseDto Execute(IGame game)
        {
            game.Stop();
            game.Start(_size, new ClassicGameMode(game, _physics, _stateTracker));

            return Success;
        }
    }
}