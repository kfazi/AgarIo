namespace AgarIo.Contract.PlayerCommands
{
    using Newtonsoft.Json;

    public class PlayerCommandDto
    {
        [JsonConstructor]
        protected PlayerCommandDto(PlayerCommandType type)
        {
            Type = type;
        }

        public PlayerCommandType Type { get; private set; }
    }
}