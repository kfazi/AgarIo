namespace AgarIo.Server.Logic.Tests
{
    using AgarIo.SystemExtension;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class GameTests
    {
#if false
        private Mock<IRandom> _randomMock;

        private Mock<IPhysics> _physicsMock;

        private Mock<IGameMode> _gameModeMock;

        private Mock<Blob> _blobMock;

        private Game _game;

        [SetUp]
        public void Setup()
        {
            _randomMock = new Mock<IRandom>();
            _physicsMock = new Mock<IPhysics>();
            _gameModeMock = new Mock<IGameMode>();
            _game = new Game(_randomMock.Object, _physicsMock.Object);
            _blobMock = new Mock<Blob>(_game);
        }

        [Test]
        public void ShouldInitializeGameMode()
        {
            _game.Start(100, _gameModeMock.Object);

            _gameModeMock.Verify(x => x.OnStart(), Times.Once);
        }

        [Test]
        public void ShouldUpdateGameMode()
        {
            _game.Start(100, _gameModeMock.Object);

            _game.Update();

            _gameModeMock.Verify(x => x.OnUpdate(), Times.Once);
        }

        [Test]
        public void ShouldRemoveFoodAndReturnItsPosition()
        {
            var foodBlob = new FoodBlob(_game) { Position = new Vector(50, 50) };
            _game.AddBlob(foodBlob);

            var position = _game.RemoveFoodAndGetSpawnPosition();

            Assert.AreEqual(new Vector(50, 50), position);
        }

        [Test]
        public void ShouldReturnRandomPositionWhenNoFoodExists()
        {
            _randomMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            var position = _game.RemoveFoodAndGetSpawnPosition();

            Assert.AreEqual(new Vector(10, 10), position);
            _randomMock.Verify(x => x.Next(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
        }

        [Test]
        public void ShouldReturnRandomPosition()
        {
            _randomMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            var position = _game.GetRandomPosition();

            Assert.AreEqual(new Vector(10, 10), position);
            _randomMock.Verify(x => x.Next(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
        }

        [Test]
        public void ShouldMoveBlobs()
        {
            _game.Start(100, _gameModeMock.Object);
            _game.AddBlob(_blobMock.Object);

            _game.Update();

            _blobMock.Verify(x => x.UpdatePosition(), Times.Once);
        }

        [Test]
        public void ShouldUpdateBlobs()
        {
            _game.Start(100, _gameModeMock.Object);
            _game.AddBlob(_blobMock.Object);

            _game.Update();

            _blobMock.Verify(x => x.Update(), Times.Once);
        }
#endif
    }
}