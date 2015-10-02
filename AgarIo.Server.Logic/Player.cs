namespace AgarIo.Server.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using AgarIo.Server.Logic.Blobs;

    public class Player
    {
        private static readonly object BlobsLock = new object();

        private readonly List<PlayerBlob> _blobs;

        private readonly AutoResetEvent _blockEvent;

        public Player(string name, string password)
        {
            Name = name;
            Password = password;

            _blobs = new List<PlayerBlob>();
            _blockEvent = new AutoResetEvent(false);

            PlayerDecisions = new PlayerDecisions();
        }

        public bool Join { get; set; }

        public string Name { get; }

        public string Password { get; }

        public PlayerDecisions PlayerDecisions { get; }

        public IReadOnlyList<PlayerBlob> Blobs
        {
            get
            {
                lock (BlobsLock)
                {
                    return _blobs.Where(blob => blob.IsReady).ToArray();
                }
            }
        }

        public void WaitForTick()
        {
            _blockEvent.WaitOne();
        }

        public void SignalTick()
        {
            _blockEvent.Set();
        }

        internal void AddBlob(PlayerBlob blob)
        {
            lock (BlobsLock)
            {
                _blobs.Add(blob);
            }
        }

        internal void RemoveBlob(PlayerBlob blob)
        {
            lock (BlobsLock)
            {
                _blobs.Remove(blob);
            }
        }

        internal void Clear()
        {
            lock (BlobsLock)
            {
                _blobs.Clear();
            }
        }
    }
}