namespace AgarIo.Contract.PlayerCommands
{
    using System;

    public class JoinPlayerCommandDto : PlayerCommandDto
    {
        public JoinPlayerCommandDto() : base(PlayerCommandType.Join)
        {
        }

        public Guid PlayerId { get; set; }
    }
}