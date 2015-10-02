namespace AgarIo.Contract.AdminCommands
{
    using System;

    public class StatePushDto : AdminCommandResponseDto
    {
        public int WorldSize { get; set; }

        public BlobDto[] AddedBlobs { get; set; }

        public BlobDto[] RemovedBlobs { get; set; }

        public BlobDto[] UpdatedBlobs { get; set; }

        public GameModeType GameModeType { get; set; }

        public string CustomGameModeData { get; set; }

        public DateTime TurnEndTime { get; set; }
    }
}