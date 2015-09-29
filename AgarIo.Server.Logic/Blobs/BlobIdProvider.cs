namespace AgarIo.Server.Logic.Blobs
{
    public static class BlobIdProvider
    {
        private static readonly object GeneratorLock = new object();

        private static int _currentId;

        public static int GetId()
        {
            lock (GeneratorLock)
            {
                return _currentId++;
            }
        }
    }
}