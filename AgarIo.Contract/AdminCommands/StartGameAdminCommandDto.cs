namespace AgarIo.Contract.AdminCommands
{
    public class StartGameAdminCommandDto : AdminCommandDto
    {
        public StartGameAdminCommandDto() : base(AdminCommandType.StartGame)
        {
        }

        public int Size { get; set; }
    }
}