namespace AgarIo.Server.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    public class PlayerRepository : IPlayerRepository
    {
        private static readonly object SynchronizationLock = new object();

        private readonly List<Player> _registeredPlayers;

        private readonly List<Player> _unregisteredPlayers;

        public PlayerRepository()
        {
            _registeredPlayers = new List<Player>();
            _unregisteredPlayers = new List<Player>();
        }

        public IEnumerable<Player> Players
        {
            get
            {
                lock (SynchronizationLock)
                {
                    return _registeredPlayers.Concat(_unregisteredPlayers).ToArray();
                }
            }
        } 

        public Player Register(string name, string password)
        {
            lock (SynchronizationLock)
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
            lock (SynchronizationLock)
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
            lock (SynchronizationLock)
            {
                _unregisteredPlayers.RemoveAll(player => !player.Blobs.Any());
            }
        }
    }
}