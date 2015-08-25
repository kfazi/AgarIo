namespace AgarIo.Contract.AdminCommands
{
    using Newtonsoft.Json;

    public class AdminCommandDto
    {
        [JsonConstructor]
        protected AdminCommandDto(AdminCommandType type)
        {
            Type = type;
        }

        public AdminCommandType Type { get; private set; }
    }
}