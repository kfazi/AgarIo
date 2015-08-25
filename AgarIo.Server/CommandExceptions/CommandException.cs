namespace AgarIo.Server.CommandExceptions
{
    using System;

    public class CommandException : Exception
    {
        public CommandException(CommandErrorCode errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public CommandErrorCode ErrorCode { get; set; }
    }
}