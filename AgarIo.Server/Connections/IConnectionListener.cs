namespace AgarIo.Server.Connections
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IConnectionListener
    {
        Task RunAsync(CancellationToken cancellationToken);

        void Update();
    }
}