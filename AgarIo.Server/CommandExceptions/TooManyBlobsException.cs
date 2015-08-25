namespace AgarIo.Server.CommandExceptions
{
    public class TooManyBlobsException : CommandException
    {
        public TooManyBlobsException()
            : base(CommandErrorCode.TooManyBlobs, "Too many blobs")
        {
        }
    }
}