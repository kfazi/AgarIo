namespace AgarIo.Server.Logic.Physics.Chipmunk
{
    using ChipmunkSharp;

    public class ChipmunkBody : IBody
    {
        public ChipmunkBody(cpCircleShape shape, bool isStatic)
        {
            Instance = shape;
            IsStatic = isStatic;
        }

        public cpCircleShape Instance { get; set; }

        public bool IsStatic { get; }

        public bool IsReady => Instance.space != null;

        public float Mass
        {
            get { return Instance.GetMass(); }
            set { Instance.SetMass(value); }
        }

        public float Radius
        {
            get { return Instance.GetRadius(); }
            set { Instance.SetRadius(value); }
        }

        public Vector LinearVelocity
        {
            get { return ChipmunkVectorConverter.FromChipmunk(Instance.body.GetVelocity()); }
            set { Instance.body.SetVelocity(ChipmunkVectorConverter.ToChipmunk(value)); }
        }

        public Vector Position
        {
            get { return ChipmunkVectorConverter.FromChipmunk(Instance.body.GetPosition()); }
            set { Instance.body.SetPosition(ChipmunkVectorConverter.ToChipmunk(value)); }
        }
    }
}