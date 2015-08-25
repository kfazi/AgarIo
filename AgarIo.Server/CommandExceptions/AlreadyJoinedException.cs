namespace AgarIo.Server.CommandExceptions
{
    public class AlreadyJoinedException : CommandException
    {
        public AlreadyJoinedException()
            : base(CommandErrorCode.AlreadyJoined, "Player already joined")
        {
        }
    }
}