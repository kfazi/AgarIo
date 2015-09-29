namespace AgarIo.Server.PlayerCommands
{
    using System.Linq;

    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.CommandExceptions;
    using AgarIo.Server.Logic;

    public class EjectMassPlayerCommand : PlayerCommand
    {
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

            if (player.Blobs.All(blob => blob.Mass < game.Settings.MinMassEject))
            {
                throw new TooLowMassException();
            }
        }

        public override CommandResponseDto Execute(Player player, IGame game)
        {
            player.PlayerDecisions.Activity = Activity.EjectMass;

            return Success;
        }
    }
}