namespace AgarIo.Server.Logic.Blobs
{
    using AgarIo.Server.Logic.Physics;

    public class EjectedMassBlob : FoodBlob
    {
        public EjectedMassBlob(IGame game, IPhysics physics, IStateTracker stateTracker, Vector position)
            : base(game, physics, stateTracker, position)
        {
            MakeDynamic();
        }
    }
}