using System;
using System.Collections;
using Minesweeper;
using NUnit.Framework;
using FakeItEasy;
using System.Linq;

namespace MinesweeperTests
{
    class GameBoardCreatorTests
    {

        IGameBoardCreator creator;

        [SetUp]
        public void setup()
        {
            creator = new GameBoardCreator(new RandomInteger());
        }

        [Test]
        [TestCase(1, 2, 3, 3)]
        [TestCase(-2, 15, 3, 15)]
        [TestCase(5, 1, 5, 3)]
        [TestCase(60, 75, 50, 50)]
        [TestCase(40, 60, 40, 50)]
        [TestCase(60, 40, 50, 40)]
        public void GivenInvalidDimensions_ThenGeneratorCreatesClosestValidBoard(int dimX, int dimY, int expectedDimX, int expectedDimY)
        {
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
            var board = creator.GenerateGameBoard(dimX, dimY);

            Assert.That(board.GetLength(0) == expectedDimX);
            Assert.That(board.GetLength(1) == expectedDimY);
        }

        [Test]
        [TestCase(20,25)]
        [TestCase(3, 3)]
        public void GivenGameBoard_ContainsMines(int dimX, int dimY)
        {
            var board = creator.GenerateGameBoard(dimX, dimY);

            bool hasMines = false;
            foreach (var item in board)
            {
                if (item.HasFlag(Field.Mine))
                    hasMines = true;
            }
            Assert.True(hasMines);
        }

        [Test]
        [TestCase(3, 3, 50)]
        [TestCase(40, 20, 50)]
        [TestCase(50, 50, 50)]
        [TestCase(50, 50, 60)]
        [TestCase(50, 50, 60)]
        [TestCase(30, 31, 20)]
        [TestCase(31, 30, 20)]
        public void GivenGameBoard_ContainsValidPercentOfMines(int dimX, int dimY, int percent)
        {
            IRandomInteger randomGenerator = A.Fake<IRandomInteger>();
            A.CallTo(() => randomGenerator.NextInt()).Returns(percent);
            IGameBoardCreator creator = new GameBoardCreator(randomGenerator);
            var board = creator.GenerateGameBoard(dimX, dimY);
            int mineCount = 0;
            int expectedCount = (int) ((dimX * dimY) * (percent / 100.0));

            for (int i = 0; i < dimX; i++)
            {
                for (int j = 0; j < dimY; j++)
                {
                    if (board[i, j].HasFlag(Field.Mine))
                        mineCount++;
                }
            }

            Assert.That(mineCount == expectedCount, $"{mineCount} neq {expectedCount}");
        }
        
        bool CheckArea(Field[,] board, int i, int j)
        {
            Field mines = Field.One;
            for (int x = i - 1; x <= i + 1; x++)
            {
                if (x < 0 || board.GetLength(0) <= x)
                    continue;

                for (int y = j - 1; y <= j + 1; y++)
                {
                    if (y < 0 || board.GetLength(1) <= y)
                        continue;

                    if (board[x, y].HasFlag(Field.Mine))
                        mines = (Field)((int)mines >> 1);
                }
            }
            mines = (Field)((int)mines << 1);

            return board[i, j].HasFlag(mines);
        }

        [Test]
        [TestCase(3, 3)]
        [TestCase(10, 10)]
        [TestCase(5, 12)]
        [TestCase(47, 15)]
        public void GivenGameBoard_ThenEmptyFieldsContainCorrectNumbers(int dimX, int dimY)
        {
            Field[,] generatedBoard = creator.GenerateGameBoard(dimX, dimY);

            for (int i = 0; i < dimX; i++)
            {
                for (int j = 0; j < dimY; j++)
                {
                    if (!generatedBoard[i, j].HasFlag(Field.Mine))
                        Assert.True(CheckArea(generatedBoard, i, j), $"index i: {i}, index j: {j}");
                }
            }
        }
    }
}
