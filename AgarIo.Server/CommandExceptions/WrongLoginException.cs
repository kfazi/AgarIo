namespace AgarIo.Server.CommandExceptions
{
    public class WrongLoginException : CommandException
    {
        public WrongLoginException()
            : base(CommandErrorCode.WrongLogin, "Wrong login")
        {
        }
    }
}