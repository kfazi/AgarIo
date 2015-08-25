namespace AgarIo.Server.Logic
{
    using System.Collections.Generic;

    using AgarIo.Server.Logic.Blobs;
    using AgarIo.Server.Logic.GameModes;
    using AgarIo.Server.Logic.Physics;
    using AgarIo.SystemExtension;

    public interface IGame
    {
        bool IsStarted { get; }

        ulong TickCount { get; }

        IReadOnlyList<Blob> Blobs { get; }

        WorldSettings Settings { get; }

        IRandom Random { get; }

        int Size { get; }

        void Start(int size, IGameMode gameMode);

        void Stop();

        void Update();

        void AddBlob(Blob blob);

        void RemoveBlob(Blob blob);

        Vector RemoveFoodAndGetSpawnPosition();

        Vector GetRandomPosition();
    }
}