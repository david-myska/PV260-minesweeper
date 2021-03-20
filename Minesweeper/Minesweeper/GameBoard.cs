using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class GameBoard : IGameBoard
    {
        private Field[,] _board;

        public GameBoard(IGameBoardCreator creator, int dimX, int dimY)
        {
            _board = creator.GenerateGameBoard(dimX, dimY);
        }

        public void Choose(int i, int j)
        {
            i--;
            j--;
            if (_board[i, j].HasFlag(Field.Covered))
                _board[i, j] &= ~(Field.Covered);

            if (_board[i, j].HasFlag(Field.Mine))
            {
                //boom
            }

            if (_board[i, j].HasFlag(Field.Zero))
            {
                TraverseNeighbors(i, j);
            }
        }

        private void TraverseNeighbors(int i, int j)
        {
            if (_board[i, j].HasFlag(Field.Covered))
                _board[i, j] &= ~(Field.Covered);

            if (_board[i, j].HasFlag(Field.Zero))
            {
                for (int x = i - 1; x <= i + 1; x++)
                {
                    if (x < 0 || _board.GetLength(0) <= x)
                        continue;

                    for (int y = j - 1; y <= j + 1; y++)
                    {
                        if (y < 0 || _board.GetLength(1) <= y)
                            continue;

                        if (_board[x, y].HasFlag(Field.Covered))
                        {
                            TraverseNeighbors(x, y);
                        }
                    }
                }
            }

        }

        public Field Get(int i, int j)
        {
            return _board[i - 1, j - 1];
        }

    }
}
