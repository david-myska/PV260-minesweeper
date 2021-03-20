using Minesweeper;
using NUnit.Framework;
using FakeItEasy;

namespace MinesweeperTests
{
    public class GameBoardTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenBoardWithMine_ThenGetReturnsMine()
        {
            IGameBoardCreator gameBoardCreator = A.Fake<IGameBoardCreator>();
            A.CallTo(() => gameBoardCreator.GenerateGameBoard(1, 1)).Returns(new Field[1, 1] { { Field.Mine } });

            IGameBoard gameBoard = new GameBoard(gameBoardCreator, 1, 1);

            Assert.That(gameBoard.Get(1, 1) == Field.Mine);
        }
    }
}