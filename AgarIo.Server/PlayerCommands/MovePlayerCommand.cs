namespace AgarIo.Server.PlayerCommands
{
    using System.Linq;

    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.CommandExceptions;
    using AgarIo.Server.Logic;
    using AgarIo.Server.Logic.Physics;

    public class MovePlayerCommand : PlayerCommand
    {
        private readonly Vector _direction;

        public MovePlayerCommand(float dx, float dy)
        {
            _direction = new Vector(dx, dy);
        }

        public override void Validate(Player player, IGame game)
        {
            if (!game.IsStarted)
            {
                throw new GameNotStartedException();
            }

            if (!player.Blobs.Any() && !player.Join)
            {
                throw new NotJoinedException();
            }
        }

        public override CommandResponseDto Execute(Player player, IGame game)
        {
            player.PlayerDecisions.Velocity = _direction;

            return Success;
        }
    }
}