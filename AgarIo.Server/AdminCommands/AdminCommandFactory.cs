namespace AgarIo.Server.AdminCommands
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using AgarIo.Contract.AdminCommands;
    using AgarIo.Server.Infrastructure;
    using AgarIo.Server.Logic;
    using AgarIo.Server.Logic.Physics;
    using AgarIo.SystemExtension;

    using AutoMapper;

    public class AdminCommandFactory : IAdminCommandFactory
    {
        private readonly IPhysics _physics;

        private readonly IStateTracker _stateTracker;

        private readonly IPlayerRepository _playerRepository;

        public AdminCommandFactory(IPhysics physics, IStateTracker stateTracker, IPlayerRepository playerRepository)
        {
            _physics = physics;
            _stateTracker = stateTracker;
            _playerRepository = playerRepository;
        }

        public async Task<AdminCommand> CreateAsync(TextReader reader, CancellationToken cancellationToken)
        {
            var json = await reader.ReadJsonAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            var adminCommandDto = json.FromJson<AdminCommandDto>();
            switch (adminCommandDto.Type)
            {
                case AdminCommandType.StartGame:
                    var startGameAdminCommandDto = json.FromJson<StartGameAdminCommandDto>();
                    return new StartGameAdminCommand(startGameAdminCommandDto.Size, _physics, _stateTracker, _playerRepository);
                case AdminCommandType.StopGame:
                    return new StopGameAdminCommand();
                case AdminCommandType.GetSnapshot:
                    return new GetSnapshotAdminCommand();
                case AdminCommandType.StartPushingState:
                    return new StartPushingStateAdminCommand();
                case AdminCommandType.UpdateSettings:
                    var updateSettingsAdminCommandDto = json.FromJson<UpdateSettingsAdminCommandDto>();
                    var settings = Mapper.Map<WorldSettings>(updateSettingsAdminCommandDto.Settings);
                    return new UpdateSettingsAdminCommand(settings);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}