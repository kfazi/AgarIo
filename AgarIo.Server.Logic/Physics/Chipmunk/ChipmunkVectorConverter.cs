namespace AgarIo.Server.Logic.Physics.Chipmunk
{
    using ChipmunkSharp;

    public static class ChipmunkVectorConverter
    {
        public static cpVect ToChipmunk(Vector vector)
        {
            return new cpVect(vector.X, vector.Y);
        }

        public static Vector FromChipmunk(cpVect vector)
        {
            return new Vector(vector.x, vector.y);
        }
    }
}