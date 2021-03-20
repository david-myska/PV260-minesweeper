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
            FillCovered(field);

            int rand = numberGenerator.NextInt();
            if (rand > 60 || rand < 20)
                rand = rand % 41 + 20;

            int numberOfMines = (int)((dimX * dimY) * (rand / 100.0));

            FillMines(field, numberOfMines, dimX, dimY);

            FillNumbers(field);

            return field;
        }

        private void FillCovered(Field[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = Field.Covered;
                }
            }
        }

        private void FillNumbers(Field[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (!board[i, j].HasFlag(Field.Mine))
                        CountMinesInArea(board, i, j);
                }
            }
        }

        private void CountMinesInArea(Field[,] board, int i, int j)
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
            board[i, j] |= mines;
        }

        private void FillMines(Field[,] field, int numberOfMines, int dimX, int dimY)
        {
            for (int i = numberOfMines; i > 0; i--)
            {
                int randXPosition = numberGenerator.NextInt() % dimX;
                int randYPosition = numberGenerator.NextInt() % dimY;


                while (field[randXPosition, randYPosition].HasFlag(Field.Mine))
                {
                    randXPosition = AdjustPosition(randXPosition, dimX);
                    if (randXPosition == 0)
                        randYPosition = AdjustPosition(randYPosition, dimY);
                }

                field[randXPosition, randYPosition] |= Field.Mine;
            }
        }

        private int AdjustPosition(int pos, int dim)
        {
            pos++;
            if (pos == dim)
            {
                pos = 0;
            }
            return pos;
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
