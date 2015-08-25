namespace AgarIo.Server.Tests.Infrastructure
{
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using AgarIo.Server.Infrastructure;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class JsonTextReaderExtensionsTests
    {
        [Test]
        public async Task ShouldReadMultilineJson()
        {
            var multilineJson = new StringBuilder();

            multilineJson.AppendLine(@"{");
            multilineJson.AppendLine(@"  number: 10,");
            multilineJson.AppendLine(@"  text: ""value""");
            multilineJson.AppendLine(@"}");

            using (var stringReader = new StringReader(multilineJson.ToString()))
            {
                var json = await stringReader.ReadJsonAsync(CancellationToken.None);

                Assert.AreEqual(multilineJson.ToString(), json);
            }
        }

        [Test]
        public void ShouldThrowWhenJsonMalformed()
        {
            var multilineJson = new StringBuilder();

            multilineJson.AppendLine(@"{");
            multilineJson.AppendLine(@"  10");
            multilineJson.AppendLine(@"}");

            using (var stringReader = new StringReader(multilineJson.ToString()))
            {
                Assert.Throws<JsonReaderException>(async () => await stringReader.ReadJsonAsync(CancellationToken.None));
            }
        }
    }
}
