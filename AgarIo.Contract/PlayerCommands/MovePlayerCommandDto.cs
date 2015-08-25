namespace AgarIo.Contract.PlayerCommands
{
    public class MovePlayerCommandDto : PlayerCommandDto
    {
        public MovePlayerCommandDto() : base(PlayerCommandType.Move)
        {
        }

        public double Dx { get; set; }

        public double Dy { get; set; }
    }
}