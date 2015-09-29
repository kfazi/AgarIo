namespace AgarIo.Server.Logic.Blobs
{
    using AgarIo.Server.Logic.Physics;

    public class FoodBlob : Blob
    {
        public FoodBlob(IGame game, IPhysics physics, IStateTracker stateTracker, Vector position)
            : base(game, physics, stateTracker, position, game.Settings.FoodMass)
        {
            MakeStatic();
        }
    }
}