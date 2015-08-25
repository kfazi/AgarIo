namespace AgarIo.Server.CommandExceptions
{
    public class CommandsLimitExceededException : CommandException
    {
        public CommandsLimitExceededException()
            : base(CommandErrorCode.CommandsLimitExceeded, "Commands limit exceeded")
        {
        }
    }
}