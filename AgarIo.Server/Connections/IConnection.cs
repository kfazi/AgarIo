namespace AgarIo.Server.Connections
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using AgarIo.Server.Logic;

    public interface IConnection
    {
        Task RunAsync(
            IGame game,
            TextReader reader,
            TextWriter writer,
            CancellationTokenSource cancellationTokenSource);
    }
}