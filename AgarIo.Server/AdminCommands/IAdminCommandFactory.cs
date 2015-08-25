namespace AgarIo.Server.AdminCommands
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAdminCommandFactory
    {
        Task<AdminCommand> CreateAsync(TextReader reader, CancellationToken cancellationToken);
    }
}