namespace AgarIo.Server.Logic.Blobs
{
    using AgarIo.Server.Logic.Physics;

    public class FoodBlob : Blob
    {
        public FoodBlob(IGame game, IPhysics physics, Vector position) : base(game, physics, position, game.Settings.FoodMass)
        {
            MakeStatic();
        }
    }
}