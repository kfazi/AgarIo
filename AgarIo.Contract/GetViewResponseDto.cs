namespace AgarIo.Contract
{
    using AgarIo.Contract.PlayerCommands;

    public class GetViewResponseDto : PlayerCommandResponseDto
    {
        public BlobDto[] Blobs { get; set; }
    }
}