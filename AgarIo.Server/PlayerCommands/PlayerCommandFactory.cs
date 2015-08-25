namespace AgarIo.Server.PlayerCommands
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.Infrastructure;
    using AgarIo.Server.Logic.Physics;
    using AgarIo.SystemExtension;

    public class PlayerCommandFactory : IPlayerCommandFactory
    {
        private readonly IPhysics _physics;

        public PlayerCommandFactory(IPhysics physics)
        {
            _physics = physics;
        }

        public async Task<PlayerCommand> CreateAsync(TextReader reader, CancellationToken cancellationToken)
        {
            var json = await reader.ReadJsonAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            var playerCommandDto = json.FromJson<PlayerCommandDto>();
            switch (playerCommandDto.Type)
            {
                case PlayerCommandType.Move:
                    var movePlayerCommandDto = json.FromJson<MovePlayerCommandDto>();
                    return new MovePlayerCommand((float)movePlayerCommandDto.Dx, (float)movePlayerCommandDto.Dy);
                case PlayerCommandType.Join:
                    return new JoinPlayerCommand(_physics);
                case PlayerCommandType.GetView:
                    return new GetViewPlayerCommand(_physics);
                case PlayerCommandType.Split:
                    return new SplitPlayerCommand();
                case PlayerCommandType.EjectMass:
                    return new EjectMassPlayerCommand();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}