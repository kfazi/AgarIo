namespace AgarIo.Server.Connections
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    using AgarIo.Contract.PlayerCommands;
    using AgarIo.Server.AdminCommands;
    using AgarIo.Server.CommandExceptions;
    using AgarIo.Server.Logic;
    using AgarIo.SystemExtension;

    public class AdminConnection : IConnection
    {
        private readonly IAdminCommandFactory _adminCommandFactory;

        private readonly BufferBlock<string> _dataToSend;

        public AdminConnection(IAdminCommandFactory adminCommandFactory)
        {
            _adminCommandFactory = adminCommandFactory;

            _dataToSend = new BufferBlock<string>();
        }

        public async Task RunAsync(IGame game, TextReader reader, TextWriter writer, CancellationTokenSource cancellationTokenSource)
        {
            var handleIncomingDataTask = HandleIncomingDataAsync(game, reader, cancellationTokenSource.Token);
            var handleOutgoingDataTask = HandleOutgoingDataAsync(writer, cancellationTokenSource.Token);
            await Task.WhenAny(handleIncomingDataTask, handleOutgoingDataTask);
            cancellationTokenSource.Cancel();
            await Task.WhenAll(handleIncomingDataTask, handleOutgoingDataTask);
        }

        private async Task HandleIncomingDataAsync(IGame game, TextReader reader, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var command = await _adminCommandFactory.CreateAsync(reader, cancellationToken);
                    if (command == null)
                    {
                        continue;
                    }

                    command.Validate(game);

                    var commandResponseDto = command.Execute(game);
                    Send(commandResponseDto);
                }
                catch (CommandException exception)
                {
                    var commandResponseDto = new CommandResponseDto
                    {
                        ErrorCode = (int)exception.ErrorCode,
                        Message = exception.Message
                    };
                    Send(commandResponseDto);
                }
            }
        }

        private async Task HandleOutgoingDataAsync(TextWriter writer, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var data = await _dataToSend.ReceiveAsync(cancellationToken);

                await writer.WriteLineAsync(data).WithCancellation(cancellationToken);
            }
        }

        private void Send(object data)
        {
            _dataToSend.Post(data.ToJson());
        }
    }
}