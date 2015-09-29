namespace AgarIo.Server.AdminCommands
{
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.Logic;

    public class UpdateSettingsAdminCommand : AdminCommand
    {
        private readonly WorldSettings _settings;

        public UpdateSettingsAdminCommand(WorldSettings settings)
        {
            _settings = settings;
        }

        public override void Validate(IGame game)
        {
        }

        public override CommandResponseDto Execute(IGame game)
        {
            game.Settings = _settings;

            return Success;
        }
    }
}