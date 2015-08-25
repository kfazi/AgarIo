namespace AgarIo.Server.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    using AgarIo.Server.Logic.Blobs;

    public class Player
    {
        private static readonly object BlobsLock = new object();

        private readonly List<PlayerBlob> _blobs;

        public Player(string name, string password)
        {
            Name = name;
            Password = password;

            _blobs = new List<PlayerBlob>();

            PlayerDecisions = new PlayerDecisions();
        }

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