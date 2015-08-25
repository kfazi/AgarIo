namespace AgarIo.Server.PlayerCommands
{
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.CommandExceptions;
    using AgarIo.Server.Logic;
    using AgarIo.Server.Logic.Blobs;
    using AgarIo.Server.Logic.Physics;

    public class JoinPlayerCommand : PlayerCommand
    {
        private readonly IPhysics _physics;

        public JoinPlayerCommand(IPhysics physics)
        {
            _physics = physics;
        }

        public override void Validate(Player player, IGame game)
        {
            if (!game.IsStarted)
            {
                throw new GameNotStartedException();
            }

            if (player.Blobs.Count > 0)
            {
                throw new AlreadyJoinedException();
            }
        }

        public override CommandResponseDto Execute(Player player, IGame game)
        {
            var position = game.RemoveFoodAndGetSpawnPosition();
            var playerBlob = new PlayerBlob(player, game, _physics, position, true);

            game.AddBlob(playerBlob);

            return Success;
        }
    }
}