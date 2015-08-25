namespace AgarIo.Contract
{
    public class BlobDto
    {
        public string Name { get; set; }

        public double Radius { get; set; }

        public VectorDto Position { get; set; }

        public BlobType Type { get; set; }
    }
}