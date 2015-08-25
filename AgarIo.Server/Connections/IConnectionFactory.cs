namespace AgarIo.Server.Connections
{
    using AgarIo.Contract;

    public interface IConnectionFactory
    {
        IConnection Create(LoginDto loginDto);
    }
}