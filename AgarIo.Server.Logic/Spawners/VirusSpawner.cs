namespace AgarIo.Server.Logic.Spawners
{
    using System;

    using AgarIo.Server.Logic.Blobs;
    using AgarIo.Server.Logic.Physics;

    public class VirusSpawner : EntitiesSpawner
    {
        public VirusSpawner(IGame game, IPhysics physics) : base(game, physics)
        {
        }

        protected override void SpawnEntity()
        {
            var position = Game.RemoveFoodAndGetSpawnPosition();

            var virusBlob = new VirusBlob(Game, Physics, position);

            Game.AddBlob(virusBlob);
        }

        protected override Type[] SpawnTypes => new[] { typeof(VirusBlob) };

        protected override uint SpawnTicksInterval => Game.Settings.VirusSpawnTicksInterval;

        protected override int SpawnAmount => 1;

        protected override int MaxSpawnAmount => Game.Settings.VirusMaxAmount;

        protected override int InitialSpawnAmount => Game.Settings.VirusMinAmount;
    }
}