using Minesweeper;

namespace Minesweeper
{
    public class GameBoardCreator : IGameBoardCreator
    {
        public Field[,] GenerateGameBoard(int dimX, int dimY)
        {
            if (dimX < 3)
                dimX = 3;
            if (dimX > 50)
                dimX = 50;

            if (dimY < 3)
                dimY = 3;
            if (dimY > 50)
                dimY = 50;

            var field = new Field[dimX, dimY];

            return field;
        }
    }
}