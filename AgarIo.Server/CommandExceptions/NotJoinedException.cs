namespace AgarIo.Server.CommandExceptions
{
    public class NotJoinedException : CommandException
    {
        public NotJoinedException()
            : base(CommandErrorCode.NotJoined, "You must join the game first")
        {
        }
    }
}