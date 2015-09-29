namespace AgarIo.Server.Connections
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IConnection
    {
        Task RunAsync(
            TextReader reader,
            TextWriter writer,
            CancellationTokenSource cancellationTokenSource);

        void Update();
    }
}