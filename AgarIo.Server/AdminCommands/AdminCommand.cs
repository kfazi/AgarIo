namespace AgarIo.Server.AdminCommands
{
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.Logic;

    public abstract class AdminCommand
    {
        protected CommandResponseDto Success => new SuccessCommandResponseDto();

        public abstract void Validate(IGame game);

        public abstract CommandResponseDto Execute(IGame game);
    }
}