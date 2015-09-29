namespace AgarIo.Server.PlayerCommands
{
    using System.Collections.Generic;
    using System.Linq;

    using AgarIo.Contract;
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.CommandExceptions;
    using AgarIo.Server.Logic;
    using AgarIo.Server.Logic.Blobs;
    using AgarIo.Server.Logic.Physics;

    using AutoMapper;

    public class GetViewPlayerCommand : PlayerCommand
    {
        private readonly IPhysics _physics;

        public GetViewPlayerCommand(IPhysics physics)
        {
            _physics = physics;
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
            var visibleBlobs = new HashSet<Blob>();

            foreach (var playerBlob in player.Blobs)
            {
                var radius = playerBlob.Radius * game.Settings.VisibilityFactor;
                _physics.ForBlobsInRange(playerBlob.Position, radius, blob => visibleBlobs.Add(blob));
            }

            var playerViewDto = new GetViewResponseDto
            {
                Blobs = Mapper.Map<BlobDto[]>(visibleBlobs)
            };

            return playerViewDto;
        }
    }
}