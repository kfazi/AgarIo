namespace AgarIo.Server.CommandExceptions
{
    public class GameNotStartedException : CommandException
    {
        public GameNotStartedException()
            : base(CommandErrorCode.GameNotStarted, "Game is not started yet")
        {
        }
    }
}