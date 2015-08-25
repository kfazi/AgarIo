namespace AgarIo.Server.Logic.Physics
{
    using System;

    using AgarIo.Server.Logic.Blobs;

    public interface IPhysics
    {
        void Start(int worldSize);

        void Stop();

        void ForBlobsInRange(Vector position, float radius, Action<Blob> action);

        IBody CreateBody(Blob blob, float radius, float mass, bool isStatic);

        void DestroyBody(IBody body);

        void Update();

        IBody MakeBodyStatic(IBody body);

        IBody MakeBodyDynamic(IBody body);
    }
}