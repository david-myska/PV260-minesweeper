using Minesweeper;
using NUnit.Framework;

namespace MinesweeperTests
{
    class GameBoardCreatorTests
    {
        [Test]
        [TestCase(1, 2, 3, 3)]
        [TestCase(-2, 15, 3, 15)]
        [TestCase(5, 1, 5, 3)]
        [TestCase(60, 75, 50, 50)]
        [TestCase(40, 60, 40, 50)]
        [TestCase(60, 40, 50, 40)]
        public void GivenWrongDimensions_ThenGeneratorCreatesClosestValidBoard(int dimX, int dimY, int expectedDimX, int expectedDimY)
        {
            IGameBoardCreator creator = new GameBoardCreator();

            var board = creator.GenerateGameBoard(dimX, dimY);

            Assert.That(board.GetLength(0) == expectedDimX);
            Assert.That(board.GetLength(1) == expectedDimY);
        }

    }
}
