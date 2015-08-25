namespace AgarIo.Server
{
    using System.Collections.Generic;

    using AgarIo.Server.Logic;

    public interface IPlayerRepository
    {
        IEnumerable<Player> Players { get; }

        Player Register(string name, string password);

        void Unregister(Player player);

        void RemoveUnregisteredAndDead();
    }
}