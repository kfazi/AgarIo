namespace AgarIo.Server.Logic.Blobs
{
    using AgarIo.Server.Logic.Physics;

    public class EjectedMassBlob : FoodBlob
    {
        public EjectedMassBlob(IGame game, IPhysics physics, Vector position) : base(game, physics, position)
        {
            MakeDynamic();
        }
    }
}