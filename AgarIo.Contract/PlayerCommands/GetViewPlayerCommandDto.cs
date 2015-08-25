namespace AgarIo.Contract.PlayerCommands
{
    public class GetViewPlayerCommandDto : PlayerCommandDto
    {
        public GetViewPlayerCommandDto() : base(PlayerCommandType.GetView)
        {
        }
    }
}