namespace AgarIo.Server.AdminCommands
{
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.Logic;

    public class StartPushingStateAdminCommand : AdminCommand
    {
        public override void Validate(IGame game)
        {
        }

        public override CommandResponseDto Execute(IGame game)
        {
            return Success;
        }
    }
}