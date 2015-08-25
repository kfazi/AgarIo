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

    public class AdminCommandFactory : IAdminCommandFactory
    {
        private readonly IPhysics _physics;

        public AdminCommandFactory(IPhysics physics)
        {
            _physics = physics;
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
                case AdminCommandType.DefineWorld:
                    var defineWorldAdminCommandDto = json.FromJson<DefineWorldAdminCommandDto>();
                    return new DefineWorldAdminCommand(defineWorldAdminCommandDto.Size, _physics);
                case AdminCommandType.GetSnapshot:
                    return new GetSnapshotAdminCommand();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}