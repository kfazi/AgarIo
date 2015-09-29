namespace AgarIo.AdminPanel.Events
{
    using AgarIo.Contract.AdminCommands;

    public class SnapshotEvent
    {
        public GetSnapshotAdminCommandResponseDto Snapshot { get; set; }
    }
}