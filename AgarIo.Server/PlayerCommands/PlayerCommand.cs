namespace AgarIo.Server.PlayerCommands
{
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.Logic;

    public abstract class PlayerCommand
    {
        protected CommandResponseDto Success => new SuccessCommandResponseDto();

        public abstract void Validate(Player player, IGame game);

        public abstract CommandResponseDto Execute(Player player, IGame game);
    }
}