namespace AgarIo.Server.Logic.Physics
{
    using System;

    public struct Vector
    {
        private const float Epsilon = 0.0001f;

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; }

        public float Y { get; }

        public float Length => (float)Math.Sqrt(X * X + Y * Y);

        public Vector Normalize()
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (X == 0 && Y == 0)
            // ReSharper restore CompareOfFloatsByEqualityOperator
            {
                return this;
            }

            var length = Length;
            return new Vector(X / length, Y / length);
        }

        public float Dist(Vector other)
        {
            return (float)Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }

        public bool Equals(Vector other)
        {
            return Math.Abs(X - other.X) < Epsilon && Math.Abs(Y - other.Y) < Epsilon;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Vector && Equals((Vector)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public static bool operator ==(Vector lhs, Vector rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Vector lhs, Vector rhs)
        {
            return !lhs.Equals(rhs);
        }

        public static Vector operator +(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Vector operator -(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static Vector operator *(Vector vector, float value)
        {
            return new Vector(vector.X * value, vector.Y * value);
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}