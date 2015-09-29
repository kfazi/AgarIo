namespace AgarIo.Server.Logic
{
    using System.Collections.Generic;

    using AgarIo.Server.Logic.Blobs;

    public class StateTracker : IStateTracker
    {
        private static readonly object SynchronizationLock = new object();

        private readonly List<Blob> _addedBlobs;

        private readonly List<Blob> _removedBlobs;

        private readonly List<Blob> _updatedBlobs;

        public StateTracker()
        {
            _addedBlobs = new List<Blob>();
            _removedBlobs = new List<Blob>();
            _updatedBlobs = new List<Blob>();
        }

        public IReadOnlyList<Blob> AddedBlobs
        {
            get
            {
                lock (SynchronizationLock)
                {
                    return _addedBlobs.ToArray();
                }
            }
        }

        public IReadOnlyList<Blob> RemovedBlobs
        {
            get
            {
                lock (SynchronizationLock)
                {
                    return _removedBlobs.ToArray();
                }
            }
        }

        public IReadOnlyList<Blob> UpdatedBlobs
        {
            get
            {
                lock (SynchronizationLock)
                {
                    return _updatedBlobs.ToArray();
                }
            }
        }

        public void AddBlob(Blob blob)
        {
            lock (SynchronizationLock)
            {
                if (_removedBlobs.Contains(blob))
                {
                    return;
                }

                _updatedBlobs.Remove(blob);
                _addedBlobs.Add(blob);
            }
        }

        public void RemoveBlob(Blob blob)
        {
            lock (SynchronizationLock)
            {
                _addedBlobs.Remove(blob);
                _updatedBlobs.Remove(blob);
                _removedBlobs.Add(blob);
            }
        }

        public void UpdateBlob(Blob blob)
        {
            lock (SynchronizationLock)
            {
                if (_addedBlobs.Contains(blob) || _removedBlobs.Contains(blob))
                {
                    return;
                }

                _updatedBlobs.Add(blob);
            }
        }

        public void Reset()
        {
            lock (SynchronizationLock)
            {
                _addedBlobs.Clear();
                _removedBlobs.Clear();
                _updatedBlobs.Clear();
            }
        }
    }
}