namespace AgarIo.Contract.AdminCommands
{
    public class DefineWorldAdminCommandDto : AdminCommandDto
    {
        public DefineWorldAdminCommandDto() : base(AdminCommandType.DefineWorld)
        {
        }

        public int Size { get; set; }
    }
}