namespace AgarIo.Server.Logic
{
    using System.Collections.Generic;

    using AgarIo.Server.Logic.Blobs;

    public interface IStateTracker
    {
        IReadOnlyList<Blob> AddedBlobs { get; }

        IReadOnlyList<Blob> RemovedBlobs { get; }

        IReadOnlyList<Blob> UpdatedBlobs { get; }

        void AddBlob(Blob blob);

        void RemoveBlob(Blob blob);

        void UpdateBlob(Blob blob);

        void Reset();
    }
}