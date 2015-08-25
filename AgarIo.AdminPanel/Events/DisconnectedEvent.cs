namespace AgarIo.AdminPanel.Events
{
    public class DisconnectedEvent
    {
        public DisconnectedEvent(string message)
        {
            Message = message;
        }

        public DisconnectedEvent()
        {
            Message = string.Empty;
        }

        public string Message { get; }
    }
}