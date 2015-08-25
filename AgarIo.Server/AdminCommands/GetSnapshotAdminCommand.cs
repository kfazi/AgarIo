namespace AgarIo.Server.AdminCommands
{
    using System;

    using AgarIo.Contract;
    using AgarIo.Contract.AdminCommands;
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.Logic;

    using AutoMapper;

    public class GetSnapshotAdminCommand : AdminCommand
    {
        public override void Validate(IGame game)
        {
        }

        public override CommandResponseDto Execute(IGame game)
        {
            var getSnapshotAdminCommandResponseDto = new GetSnapshotAdminCommandResponseDto
            {
                WorldSize = game.Size,
                WorldSettings = Mapper.Map<WorldSettingsDto>(game.Settings),
                Blobs = Mapper.Map<BlobDto[]>(game.Blobs)
            };

            return getSnapshotAdminCommandResponseDto;
        }
    }
}