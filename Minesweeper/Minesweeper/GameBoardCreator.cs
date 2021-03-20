using System;

namespace Minesweeper
{
    public class GameBoardCreator : IGameBoardCreator
    {
        private IRandom numberGenerator;

        public GameBoardCreator(IRandom next)
        {
            numberGenerator = next;
        }

        public Field[,] GenerateGameBoard(int dimX, int dimY)
        {
            dimX = AdjustDimension(dimX);
            dimY = AdjustDimension(dimY);

            var field = new Field[dimX, dimY];
            int rand = numberGenerator.NextInt();
            if (rand > 60 || rand < 20)
                rand = rand % 41 + 20;

            int numberOfMines = (int)((dimX * dimY) * (rand / 100.0));

            FillMines(field, numberOfMines, dimX, dimY);

            return field;
        }

        private void FillMines(Field[,] field, int numberOfMines, int dimX, int dimY)
        {
            for (int i = numberOfMines; i > 0; i--)
            {
                int randXPosition = numberGenerator.NextInt() % dimX;
                int randYPosition = numberGenerator.NextInt() % dimY;


                while (field[randXPosition, randYPosition] == Field.Mine)
                {
                    randXPosition++;
                    if (randXPosition == dimX)
                    {
                        randXPosition = 0;
                        randYPosition++;

                        if (randYPosition == dimY)
                        {
                            randYPosition = 0;
                        }
                    }
                }

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
