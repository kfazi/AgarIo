namespace AgarIo.Server.PlayerCommands
{
    using System.Linq;

    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.CommandExceptions;
    using AgarIo.Server.Logic;
    using AgarIo.Server.Logic.Blobs;

    public class SplitPlayerCommand : PlayerCommand
    {
        public override void Validate(Player player, IGame game)
        {
            if (!game.IsStarted)
            {
                throw new GameNotStartedException();
            }

            if (!player.Blobs.Any())
            {
                throw new NotJoinedException();
            }

            if (player.Blobs.Count >= game.Settings.MaxPlayerBlobCount)
            {
                throw new TooManyBlobsException();
            }

            if (player.Blobs.All(blob => blob.Mass < game.Settings.MinMassSplit))
            {
                throw new TooLowMassException();
            }
        }

        public override CommandResponseDto Execute(Player player, IGame game)
        {
            player.PlayerDecisions.Activity = Activity.Split;

            return Success;
        }
    }
}