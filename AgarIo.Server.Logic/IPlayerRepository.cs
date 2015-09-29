namespace AgarIo.Server.Logic
{
    using System.Collections.Generic;

    public interface IPlayerRepository
    {
        IEnumerable<Player> Players { get; }

        Player Register(string name, string password);

        void Unregister(Player player);

        void RemoveUnregisteredAndDead();
    }
}