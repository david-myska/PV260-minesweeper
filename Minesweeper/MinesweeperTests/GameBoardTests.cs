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
        [TestCase(Field.Covered)]
        [TestCase(Field.Mine)]
        [TestCase(Field.Flagged)]
        public void GivenBoardWithMine_ThenGetReturnsMine(Field field)
        {
            IGameBoardCreator gameBoardCreator = A.Fake<IGameBoardCreator>();
            A.CallTo(() => gameBoardCreator.GenerateGameBoard(1, 1)).Returns(new Field[1, 1] { { field } });

            IGameBoard gameBoard = new GameBoard(gameBoardCreator, 1, 1);

            Assert.That(gameBoard.Get(1, 1) == field);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 1)]
        [TestCase(3, 2)]
        [TestCase(3, 3)]
        public void GivenNonTrivialBoard_GetReturnsCorrectPosition(int posX, int posY)
        {
            IGameBoardCreator gameBoardCreator = A.Fake<IGameBoardCreator>();
            A.CallTo(() => gameBoardCreator.GenerateGameBoard(3, 3)).Returns(new Field[3, 3] {                 
                { Field.Covered, Field.One, Field.One },
                { Field.Covered, Field.Flagged, Field.Two},
                { Field.Covered, Field.Covered, Field.Covered}
            });

            Field[,] generatedBoard = gameBoardCreator.GenerateGameBoard(3, 3);

            IGameBoard gameBoard = new GameBoard(gameBoardCreator, 3, 3);

            Assert.That(gameBoard.Get(posX, posY) == generatedBoard[posX - 1, posY - 1]);
        }

        //Test for out of bounds positions 

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 3)]
        [TestCase(2, 1)]
        public void GivenCoveredBoard_ThenFieldsGetsUncovered(int posX, int posY)
        {
            IGameBoardCreator gameBoardCreator = A.Fake<IGameBoardCreator>();
            A.CallTo(() => gameBoardCreator.GenerateGameBoard(3, 3)).Returns(new Field[3, 3] {
                { Field.Covered, Field.Covered, Field.Covered },
                { Field.Covered, Field.Covered, Field.Covered},
                { Field.Covered, Field.Covered, Field.Covered}
            });

            IGameBoard gameBoard = new GameBoard(gameBoardCreator, 3, 3);

            gameBoard.Choose(posX, posY);

            Assert.False(gameBoard.Get(posX, posY).HasFlag(Field.Covered));
        }
    }
}