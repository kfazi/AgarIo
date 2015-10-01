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

        private bool _initialSpawn;

        protected EntitiesSpawner(IGame game, IPhysics physics)
        {
            Game = game;
            Physics = physics;
        }

        public void Initialize()
        {
            _initialSpawn = true;
        }

        public void Spawn()
        {
            if (_initialSpawn)
            {
                for (var i = 0; i < InitialSpawnAmount; i++)
                {
                    SpawnEntity();
                }

                _initialSpawn = false;

                _lastSpawnTick = Game.TickCount;
                return;
            }

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