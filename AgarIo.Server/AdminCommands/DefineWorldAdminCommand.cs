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

        public DefineWorldAdminCommand(int size, IPhysics physics)
        {
            _size = size;
            _physics = physics;
        }

        public override void Validate(IGame game)
        {
        }

        public override CommandResponseDto Execute(IGame game)
        {
            game.Stop();
            game.Start(_size, new ClassicGameMode(game, _physics));

            return Success;
        }
    }
}