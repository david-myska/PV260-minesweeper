using Minesweeper;

namespace Minesweeper
{
    public class GameBoardCreator : IGameBoardCreator
    {
        public Field[,] GenerateGameBoard(int dimX, int dimY)
        {
            return new Field[,] { { Field.EmptyCovered } };
        }
    }
}