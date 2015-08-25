namespace AgarIo.Server.Tests.PlayerCommands
{
    using System;
    using System.Linq;

    using AgarIo.Contract;
    using AgarIo.Server.Logic;
    using AgarIo.Server.PlayerCommands;

    using NUnit.Framework;

    [TestFixture]
    public class GetViewPlayerCommandTests
    {
        /*
        [Test]
        public void ShouldSendOnlyVisibleGoos()
        {
            var autoMapperSetup = new AutoMapperSetup();
            autoMapperSetup.Run();

            object response = null;
            Action<object> sendResponseMock = obj => response = obj;

            var getViewPlayerCommand = new GetViewPlayerCommand();

            var firstPlayer = new Player("first player name");
            var secondPlayer = new Player("second player name");
            var thirdPlayer = new Player("third player name");

            var world = new Game(100, 1, 5);

            world.AddPlayer(firstPlayer);
            world.AddPlayer(secondPlayer);
            world.AddPlayer(thirdPlayer);

            var firstPlayerGoo = firstPlayer.Blobs.First();
            var secondPlayerGoo = secondPlayer.Blobs.First();
            var thirdPlayerGoo = thirdPlayer.Blobs.First();

            firstPlayerGoo.Position = new Vector(8, 0);
            secondPlayerGoo.Position = new Vector(0, 0);
            thirdPlayerGoo.Position = new Vector(4, 0);

            getViewPlayerCommand.Execute(firstPlayer, sendResponseMock, world);

            Assert.IsInstanceOf<GetViewResponseDto>(response);
            Assert.IsTrue(((GetViewResponseDto)response).Blobs.Any(goo => goo.Name == "first player name"), "First player goo is missing");
            Assert.IsTrue(((GetViewResponseDto)response).Blobs.Any(goo => goo.Name == "third player name"), "Third player goo is missing");
            Assert.IsFalse(((GetViewResponseDto)response).Blobs.Any(goo => goo.Name == "second player name"), "Second player goo is present");
        }
        */
    }
}