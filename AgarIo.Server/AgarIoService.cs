namespace AgarIo.Server
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AgarIo.Server.Connections;
    using AgarIo.Server.Logic;

    using NLog;

    internal class AgarIoService : IAgarIoService
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly IConnectionListener _connectionListener;

        private readonly IGame _game;

        private CancellationTokenSource _cancellationTokenSource;

        private Task[] _tasks;

        public AgarIoService(IConnectionListener connectionListener, IGame game)
        {
            _connectionListener = connectionListener;
            _game = game;
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            _tasks = new[] { _connectionListener.RunAsync(cancellationToken), GameLoopAsync(cancellationToken) };
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();

            try
            {
                Task.WaitAny(_tasks);
                _cancellationTokenSource.Cancel();
                Task.WaitAll(_tasks);
            }
            catch (Exception exception) when (
                (exception is TaskCanceledException) ||
                (((exception as AggregateException)?.InnerExceptions.All(innerException => (innerException is TaskCanceledException) || (innerException is IOException))) ?? false))
            {
                // Ignore task cancellation exceptions
            }
        }

        private async Task GameLoopAsync(CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (!cancellationToken.IsCancellationRequested)
            {
                var start = stopwatch.ElapsedMilliseconds;

                _game.Update();

                var elapsed = stopwatch.ElapsedMilliseconds - start;

                Log.Info($"Update time: {elapsed}ms");
                await Task.Delay((int)Math.Max(Game.TickDurationMs - elapsed, 0), cancellationToken);
            }
        }
    }
}