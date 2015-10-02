namespace AgarIo.Server.Logic.GameModes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using AgarIo.Server.Logic.Physics;
    using AgarIo.Server.Logic.Spawners;

    public class ClassicGameMode : IGameMode
    {
        private readonly IPlayerRepository _playerRepository;

        private readonly List<EntitiesSpawner> _entitiesSpawners;

        private readonly List<PlayerStat> _playerStats;

        private int _currentPlayerId;

        public ClassicGameMode(IGame game, IPhysics physics, IStateTracker stateTracker, IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;

            _entitiesSpawners = new List<EntitiesSpawner>
            {
                new FoodSpawner(game, physics, stateTracker),
                new VirusSpawner(game, physics, stateTracker)
            };

            _playerStats = new List<PlayerStat>();
        }

        public void OnStart()
        {
            _playerStats.Clear();

            foreach (var spawner in _entitiesSpawners)
            {
                spawner.Initialize();
            }
        }

        public void OnFinish()
        {
            var statsBuilder = new StringBuilder();

            statsBuilder.AppendLine($"Game finished on {DateTime.Now}");
            foreach (var stat in _playerStats.OrderByDescending(x => x.Score))
            {
                statsBuilder.AppendLine($"{stat.Name}\t\t\t{stat.Score}");
            }

            statsBuilder.AppendLine();

            File.AppendAllText("stats.txt", statsBuilder.ToString(), Encoding.UTF8);
        }

        public void OnUpdate()
        {
            foreach (var spawner in _entitiesSpawners)
            {
                spawner.Spawn();
            }
        }

        public object GetCustomData()
        {
            var players = _playerRepository.Players.ToArray();
            _playerStats.RemoveAll(playerStat => players.All(player => player.Name != playerStat.Name));

            foreach (var player in players)
            {
                var playerStat = _playerStats.FirstOrDefault(x => x.Name == player.Name);
                if (playerStat == null)
                {
                    playerStat = new PlayerStat(_currentPlayerId++, player.Name);
                    _playerStats.Add(playerStat);
                }

                playerStat.Score = player.Blobs.Any() ? Math.Max(playerStat.Score, (int)player.Blobs.Sum(blob => blob.Mass)) : 0;
            }

            return new ClassicGameModeData(_playerStats);
        }
    }
}