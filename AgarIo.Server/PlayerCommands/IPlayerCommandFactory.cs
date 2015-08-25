namespace AgarIo.Server.PlayerCommands
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IPlayerCommandFactory
    {
        Task<PlayerCommand> CreateAsync(TextReader reader, CancellationToken cancellationToken);
    }
}