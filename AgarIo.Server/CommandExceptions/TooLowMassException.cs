namespace AgarIo.Server.CommandExceptions
{
    public class TooLowMassException : CommandException
    {
        public TooLowMassException()
            : base(CommandErrorCode.TooLowMass, "Too low mass")
        {
        }
    }
}