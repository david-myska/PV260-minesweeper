using System;

namespace Minesweeper
{
    public class GameBoardCreator : IGameBoardCreator
    {
        private IRandomInteger numberGenerator;

        public GameBoardCreator(IRandomInteger next)
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

            FillMines(field, numberOfMines);

            FillNumbers(field);

            return field;
        }

        private void FillCovered(Field[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = Field.Covered | Field.Zero;
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

            CheckMines(board, ref mines, i - 1, j - 1);
            CheckMines(board, ref mines, i - 1, j);
            CheckMines(board, ref mines, i - 1, j + 1);
            CheckMines(board, ref mines, i, j - 1);
            CheckMines(board, ref mines, i, j + 1);
            CheckMines(board, ref mines, i + 1, j - 1);
            CheckMines(board, ref mines, i + 1, j);
            CheckMines(board, ref mines, i + 1, j + 1);

            mines = (Field)((int)mines << 1);
            board[i, j] |= mines;
            board[i, j] &= ~Field.Zero;
        }

        private void CheckMines(Field[,] board, ref Field mines, int i, int j)
        {
            if ((i < 0 || board.GetLength(0) <= i) || (j < 0 || board.GetLength(1) <= j))
                return;

            if (board[i, j].HasFlag(Field.Mine))
                mines = (Field)((int)mines >> 1);
        }

        private void FillMines(Field[,] field, int numberOfMines)
        {
            for (int i = numberOfMines; i > 0; i--)
            {
                int randXPosition = numberGenerator.NextInt() % field.GetLength(0);
                int randYPosition = numberGenerator.NextInt() % field.GetLength(1);

                InsertMine(field, randXPosition, randYPosition);
            }
        }

        private void InsertMine(Field[,] field, int randXPosition, int randYPosition)
        {
            while (field[randXPosition, randYPosition].HasFlag(Field.Mine))
            {
                randXPosition = AdjustPosition(randXPosition, field.GetLength(0));
                if (randXPosition == 0)
                    randYPosition = AdjustPosition(randYPosition, field.GetLength(1));
            }

            field[randXPosition, randYPosition] |= Field.Mine;
            field[randXPosition, randYPosition] &= ~Field.Zero;
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
