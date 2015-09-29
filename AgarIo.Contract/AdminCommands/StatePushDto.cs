namespace AgarIo.Contract.AdminCommands
{
    public class StatePushDto : AdminCommandResponseDto
    {
        public int WorldSize { get; set; }

        public BlobDto[] AddedBlobs { get; set; }

        public BlobDto[] RemovedBlobs { get; set; }

        public BlobDto[] UpdatedBlobs { get; set; }
    }
}