namespace AgarIo.Contract.PlayerCommands
{
    public class SuccessCommandResponseDto : CommandResponseDto
    {
        public SuccessCommandResponseDto()
        {
            ErrorCode = 0;
            Message = null;
        }
    }
}