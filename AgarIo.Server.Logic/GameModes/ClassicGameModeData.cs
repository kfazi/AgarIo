namespace AgarIo.Server.Logic.GameModes
{
    using System.Collections.Generic;

    public class ClassicGameModeData
    {
        public ClassicGameModeData(List<PlayerStat> playerStats)
        {
            PlayerStats = playerStats;
        }

        public List<PlayerStat> PlayerStats { get; }
    }
}