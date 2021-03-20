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
        public void GivenInvalidDimensions_ThenGeneratorCreatesClosestValidBoard(int dimX, int dimY, int expectedDimX, int expectedDimY)
        {
            IGameBoardCreator creator = new GameBoardCreator();
            var board = creator.GenerateGameBoard(dimX, dimY);

            Assert.That(board.GetLength(0) == expectedDimX);
            Assert.That(board.GetLength(1) == expectedDimY);
        }

        [Test]
        [TestCase(3, 3, 3, 3)]
        [TestCase(50, 50, 50, 50)]
        [TestCase(25, 40, 25, 40)]
        public void GivenValidDimensions_ThenGeneratorCreatesValidBoard(int dimX, int dimY, int expectedDimX, int expectedDimY)
        {
            IGameBoardCreator creator = new GameBoardCreator();
            var board = creator.GenerateGameBoard(dimX, dimY);

            Assert.That(board.GetLength(0) == expectedDimX);
            Assert.That(board.GetLength(1) == expectedDimY);
        }

        [Test]
        [TestCase(20,25)]
        [TestCase(3, 3)]
        public void GivenGameBoard_ContainsMines(int dimX, int dimY)
        {
            IGameBoardCreator creator = new GameBoardCreator();
            var board = creator.GenerateGameBoard(dimX, dimY);

            Assert.Contains(Field.Mine, board);
        }
    }
}
