namespace AgarIo.Server.Logic.Spawners
{
    using System;
    using System.Linq;

    using AgarIo.Server.Logic.Physics;

    public abstract class EntitiesSpawner
    {
        protected IGame Game { get; }

        protected IPhysics Physics { get; }

        private ulong _lastSpawnTick;

        protected EntitiesSpawner(IGame game, IPhysics physics)
        {
            Game = game;
            Physics = physics;
        }

        public void Initialize()
        {
            for (var i = 0; i < InitialSpawnAmount; i++)
            {
                SpawnEntity();
            }

            _lastSpawnTick = Game.TickCount;
        }

        public void Spawn()
        {
            if (Game.Blobs.Count(blob => SpawnTypes.Any(type => type == blob.GetType())) >= MaxSpawnAmount)
            {
                return;
            }

            if (Game.TickCount < _lastSpawnTick + SpawnTicksInterval)
            {
                return;
            }

            for (var i = 0; i < SpawnAmount; i++)
            {
                SpawnEntity();
            }

            _lastSpawnTick = Game.TickCount;
        }

        protected abstract void SpawnEntity();

        protected abstract Type[] SpawnTypes { get; }

        protected abstract uint SpawnTicksInterval { get; }

        protected abstract int SpawnAmount { get; }

        protected abstract int MaxSpawnAmount { get; }

        protected abstract int InitialSpawnAmount { get; }
    }
}