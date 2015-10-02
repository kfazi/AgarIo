namespace AgarIo.Server.Logic.GameModes
{
    public interface IGameMode
    {
        void OnStart();

        void OnFinish();

        void OnUpdate();

        object GetCustomData();
    }
}