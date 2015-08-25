namespace AgarIo.SystemExtension
{
    using System;

    public class RandomWrap : IRandom
    {
        private readonly Random _random;

        public RandomWrap()
        {
            _random = new Random();
        }

        public RandomWrap(int seed)
        {
            _random = new Random(seed);
        }

        public int Next()
        {
            return _random.Next();
        }

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public double NextDouble()
        {
            return _random.NextDouble();
        }

        public void NextBytes(byte[] buffer)
        {
            _random.NextBytes(buffer);
        }
    }
}