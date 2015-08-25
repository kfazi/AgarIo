namespace AgarIo.AdminPanel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Threading;

    using AgarIo.Contract;
    using AgarIo.Contract.AdminCommands;
    using AgarIo.Contract.PlayerCommands;

    using Caliburn.Micro;

    public class Connection : IConnection, IDisposable
    {
        private readonly ConnectionThread _connectionThread;

        private readonly Thread _networkThread;

        private readonly ManualResetEvent _readyEvent;

        private Dispatcher _dispatcher;

        public Connection(IEventAggregator eventAggregator)
        {
            _connectionThread = new ConnectionThread(eventAggregator);

            _readyEvent = new ManualResetEvent(false);

            _networkThread = new Thread(NetworkThreadEntry);
            _networkThread.Start();

            _readyEvent.WaitOne();
        }

        public async Task ConnectAsync(string host, int port, CancellationToken cancellationToken)
        {
            await _dispatcher.InvokeAsync(() =>
                {
                    _connectionThread.Connect(host, port);
                },
                DispatcherPriority.Normal,
                cancellationToken);
        }

        public async Task DisconnectAsync()
        {
            await _dispatcher.InvokeAsync(() =>
            {
                _connectionThread.Disconnect();
            });
        }

        private void NetworkThreadEntry()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _readyEvent.Set();
            Dispatcher.Run();
        }

        public async Task<CommandResponseDto> DispatchLoginAsync(LoginDto loginDto, CancellationToken cancellationToken)
        {
            return await DispatchAsync<CommandResponseDto>(loginDto, cancellationToken);
        }

        public async Task<T> DispatchCommandAsync<T>(AdminCommandDto command, CancellationToken cancellationToken) where T : AdminCommandResponseDto
        {
            return await DispatchAsync<T>(command, cancellationToken);
        }

        public void Dispose()
        {
            _dispatcher.InvokeShutdown();
            _networkThread.Join();
        }

        private async Task<T> DispatchAsync<T>(object command, CancellationToken cancellationToken)
        {
            var result = default(T);
            await _dispatcher.InvokeAsync(() =>
                {
                    result = _connectionThread.SendCommand<T>(command);
                },
                DispatcherPriority.Normal,
                cancellationToken);
            return result;
        }
    }
}