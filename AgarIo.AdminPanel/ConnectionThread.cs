namespace AgarIo.AdminPanel
{
    using System.IO;
    using System.Net.Sockets;

    using AgarIo.AdminPanel.Events;
    using AgarIo.SystemExtension;

    using Caliburn.Micro;

    public class ConnectionThread
    {
        private readonly IEventAggregator _eventAggregator;

        private TcpClient _tcpClient;

        private StreamWriter _writer;

        private StreamReader _reader;

        public ConnectionThread(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Connect(string host, int port)
        {
            try
            {
                _eventAggregator.PublishOnUIThread(new ConnectingEvent());
                _tcpClient = new TcpClient();
                _tcpClient.Connect(host, port);
                _reader = new StreamReader(_tcpClient.GetStream());
                _writer = new StreamWriter(_tcpClient.GetStream()) { AutoFlush = true };
                _eventAggregator.PublishOnCurrentThread(new ConnectedEvent());
            }
            catch (SocketException exception)
            {
                _eventAggregator.PublishOnUIThread(new DisconnectedEvent(exception.Message));
            }
        }

        public void Disconnect()
        {
            _writer?.Dispose();
            _reader?.Dispose();

            if (_tcpClient != null && _tcpClient.Connected)
            {
                _tcpClient.Close();
            }

            _eventAggregator.PublishOnUIThread(new DisconnectedEvent());

            _tcpClient = null;
        }

        public T SendCommand<T>(object command)
        {
            _writer.WriteLine(command.ToJson());
            var responseJson = _reader.ReadLine();
            return responseJson.FromJson<T>();
        }
    }
}