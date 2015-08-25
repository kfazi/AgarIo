namespace AgarIo.Server.Infrastructure
{
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using AgarIo.SystemExtension;

    using Newtonsoft.Json;

    public static class JsonTextReaderExtensions
    {
        private const string EofMessage = "Unexpected end";

        public static async Task<string> ReadJsonAsync(this TextReader reader, CancellationToken cancellationToken)
        {
            var jsonLines = new StringBuilder();
            while (!cancellationToken.IsCancellationRequested)
            {
                var line = await reader.ReadLineAsync().WithCancellation(cancellationToken);
                jsonLines.AppendLine(line);

                try
                {
                    var lines = jsonLines.ToString();
                    lines.FromJson<object>();
                    return lines;
                }
                catch (JsonException exception)
                {
                    if (!exception.Message.StartsWith(EofMessage))
                    {
                        throw;
                    }
                }
            }

            return string.Empty;
        }
    }
}