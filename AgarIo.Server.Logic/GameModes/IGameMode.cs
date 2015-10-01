namespace AgarIo.Server.Logic.GameModes
{
    public interface IGameMode
    {
        void OnStart();

        void OnUpdate();

        object GetCustomData();
    }
}