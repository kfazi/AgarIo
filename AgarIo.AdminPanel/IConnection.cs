namespace AgarIo.AdminPanel
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgarIo.Contract;
    using AgarIo.Contract.AdminCommands;
    using AgarIo.Contract.PlayerCommands;

    public interface IConnection
    {
        Task ConnectAsync(string host, int port, CancellationToken cancellationToken);

        Task DisconnectAsync();

        Task<CommandResponseDto> DispatchLoginAsync(LoginDto loginDto, CancellationToken cancellationToken);

        Task<T> DispatchCommandAsync<T>(AdminCommandDto command, CancellationToken cancellationToken) where T : AdminCommandResponseDto;
    }
}