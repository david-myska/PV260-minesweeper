using System;

namespace Minesweeper
{
    public class GameBoardCreator : IGameBoardCreator
    {
        private Random numberGenerator = new Random();

        public Field[,] GenerateGameBoard(int dimX, int dimY)
        {
            dimX = AdjustDimension(dimX);
            dimY = AdjustDimension(dimY);

            var field = new Field[dimX, dimY];
            int rand = numberGenerator.Next(20,61);
            int numberOfMines = (int)((dimX * dimY) * (rand / 100.0));

            FillMines(field, numberOfMines, dimX, dimY);

            return field;
        }

        private void FillMines(Field[,] field, int numberOfMines, int dimX, int dimY)
        {
            for (int i = numberOfMines; i > 0; i--)
            {
                int randXPosition = numberGenerator.Next() % dimX;
                int randYPosition = numberGenerator.Next() % dimY;

                if (field[randXPosition, randYPosition] != Field.Mine)
                    field[randXPosition, randYPosition] = Field.Mine;
            }
        }

        private int AdjustDimension(int dim)
        {
            if (dim < 3)
                dim = 3;
            if (dim > 50)
                dim = 50;
            return dim;
        }
    }
}
