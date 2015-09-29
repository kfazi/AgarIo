namespace AgarIo.Server.PlayerCommands
{
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.CommandExceptions;
    using AgarIo.Server.Logic;

    public class JoinPlayerCommand : PlayerCommand
    {
        public override void Validate(Player player, IGame game)
        {
            if (!game.IsStarted)
            {
                throw new GameNotStartedException();
            }

            if (player.Blobs.Count > 0 || player.Join)
            {
                throw new AlreadyJoinedException();
            }
        }

        public override CommandResponseDto Execute(Player player, IGame game)
        {
            player.Join = true;

            return Success;
        }
    }
}