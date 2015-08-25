namespace AgarIo.Server.Logic.Physics
{
    public interface IBody
    {
        bool IsStatic { get; }

        bool IsReady { get; }

        float Mass { get; set; }

        float Radius { get; set; }

        Vector LinearVelocity { get; set; }

        Vector Position { get; set; }
    }
}