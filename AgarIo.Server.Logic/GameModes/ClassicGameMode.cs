namespace AgarIo.Server.Logic.GameModes
{
    using System.Collections.Generic;

    using AgarIo.Server.Logic.Physics;
    using AgarIo.Server.Logic.Spawners;

    public class ClassicGameMode : IGameMode
    {
        private readonly List<EntitiesSpawner> _entitiesSpawners;

        public ClassicGameMode(IGame game, IPhysics physics)
        {
            _entitiesSpawners = new List<EntitiesSpawner> { new FoodSpawner(game, physics), new VirusSpawner(game, physics) };
        }

        public void OnStart()
        {
            foreach (var spawner in _entitiesSpawners)
            {
                spawner.Initialize();
            }
        }

        public void OnUpdate()
        {
            foreach (var spawner in _entitiesSpawners)
            {
                spawner.Spawn();
            }
        }
    }
}