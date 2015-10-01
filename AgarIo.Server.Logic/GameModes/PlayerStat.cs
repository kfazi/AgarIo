namespace AgarIo.Server.Logic.GameModes
{
    public class PlayerStat
    {
        public PlayerStat(int id, string name)
        {
            Id = id;
            Name = name;
            Score = 0;
        }

        public int Id { get; }

        public string Name { get; }

        public int Score { get; set; }
    }
}