namespace AgarIo.Server.Tests.PlayerCommands
{
    using System.Linq;

    using AgarIo.Server.Logic;
    using AgarIo.Server.PlayerCommands;

    using NUnit.Framework;

    [TestFixture]
    public class MovePlayerCommandTests
    {
        /*
        [Test]
        public void ShouldSetMoveVectorRelativeToGoosCenter()
        {
            var player = new Player("player name");

            var world = new Game(100, 1, 1);

            world.AddPlayer(player);

            var firstGoo = player.Blobs.First();
            var secondGoo = world.CreateGoo(player);
            player.AddGoo(secondGoo);

            firstGoo.Position = new Vector(1, 0);
            secondGoo.Position = new Vector(0, 1);

            var movePlayerCommand = new MovePlayerCommand(1, 1);
            movePlayerCommand.Execute(player, x => { }, world);

            Assert.AreEqual(new Vector(0.5, 1.5), firstGoo.MoveVector);
            Assert.AreEqual(new Vector(1.5, 0.5), secondGoo.MoveVector);
        }
        */
    }
}