using System;
using System.Linq;

namespace Minesweeper
{
    public class GameBoardCreator : IGameBoardCreator
    {
        private readonly IRandomInteger numberGenerator;
        private const int LowerBound = 20;
        private const int UpperBound = 60;

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
            int rand = numberGenerator.NextInt(LowerBound, UpperBound);
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
                    board[i, j] = board[i, j].SetFlag(Field.Covered | Field.Zero);
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
            Field numberOfMines = Field.One;

            CheckMines(board, ref numberOfMines, i - 1, j - 1);
            CheckMines(board, ref numberOfMines, i - 1, j);
            CheckMines(board, ref numberOfMines, i - 1, j + 1);
            CheckMines(board, ref numberOfMines, i, j - 1);
            CheckMines(board, ref numberOfMines, i, j + 1);
            CheckMines(board, ref numberOfMines, i + 1, j - 1);
            CheckMines(board, ref numberOfMines, i + 1, j);
            CheckMines(board, ref numberOfMines, i + 1, j + 1);

            //Correct number of mines
            numberOfMines = (Field)((int)numberOfMines << 1);
            board[i, j] = board[i, j].SetFlag(numberOfMines);
            board[i, j] = board[i, j].ClearFlag(Field.Zero);
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
                int randXPosition = numberGenerator.NextInt(0, field.GetLength(0) - 1);
                int randYPosition = numberGenerator.NextInt(0, field.GetLength(1) - 1);

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

            field[randXPosition, randYPosition] = field[randXPosition, randYPosition].SetFlag(Field.Mine).ClearFlag(Field.Zero);
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
