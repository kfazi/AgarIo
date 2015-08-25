namespace AgarIo.Contract
{
    public class WorldDto
    {
        public int Size { get; set; }

        public double InitialGooSize { get; set; }

        public PlayerDto[] Players { get; set; }
    }
}