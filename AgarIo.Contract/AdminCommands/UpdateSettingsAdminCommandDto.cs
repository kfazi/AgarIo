namespace AgarIo.Contract.AdminCommands
{
    public class UpdateSettingsAdminCommandDto : AdminCommandDto
    {
        public UpdateSettingsAdminCommandDto() : base(AdminCommandType.UpdateSettings)
        {
        }

        public WorldSettingsDto Settings { get; set; }
    }
}