namespace AgarIo.Server.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    public class PlayerRepository : IPlayerRepository
    {
        private readonly object _synchronizationLock = new object();

        private readonly List<Player> _registeredPlayers;

        private readonly List<Player> _unregisteredPlayers;

        public PlayerRepository()
        {
            _registeredPlayers = new List<Player>();
            _unregisteredPlayers = new List<Player>();
        }

        public IEnumerable<Player> Players => _registeredPlayers.Concat(_unregisteredPlayers);

        public Player Register(string name, string password)
        {
            lock (_synchronizationLock)
            {
                var player = _registeredPlayers.FirstOrDefault(x => x.Name == name) ?? new Player(name, password);

                _unregisteredPlayers.Remove(player);

                if (!_registeredPlayers.Contains(player))
                {
                    _registeredPlayers.Add(player);
                }

                return player;
            }
        }

        public void Unregister(Player player)
        {
            lock (_synchronizationLock)
            {
                if (_unregisteredPlayers.Contains(player))
                {
                    return;
                }

                _registeredPlayers.Remove(player);

                if (!_unregisteredPlayers.Contains(player))
                {
                    _unregisteredPlayers.Add(player);
                }
            }
        }

        public void RemoveUnregisteredAndDead()
        {
            lock (_synchronizationLock)
            {
                _unregisteredPlayers.RemoveAll(player => !player.Blobs.Any());
            }
        }
    }
}