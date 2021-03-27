using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class GameBoard : IGameBoard
    {
        private readonly Field[,] _board;

        private bool _steppedOnMine = false;


        public GameBoard(IGameBoardCreator creator, int dimX, int dimY)
        {
            _board = creator.GenerateGameBoard(dimX, dimY);
        }

        public void Choose(int i, int j)
        {
            i--;
            j--;

            if (_board[i, j].HasFlag(Field.Flagged))
                return;

            if (_board[i, j].HasFlag(Field.Covered))
                _board[i, j] =_board[i, j].ClearFlag(Field.Covered);

            if (_board[i, j].HasFlag(Field.Mine))
            {
                _steppedOnMine = true;
            }

            if (_board[i, j].HasFlag(Field.Zero))
            {
                TraverseNeighbors(i, j, true);
            }
        }

        public void UncoverAllFields()
        {
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    _board[i, j] = _board[i, j].ClearFlag(Field.Covered);
                }
            }
        }

        public bool IsMine(int i, int j)
        {
            return _board[i - 1, j - 1].HasFlag(Field.Mine);
        }

        public bool IsFlagged(int i, int j)
        {
            return _board[i - 1, j - 1].HasFlag(Field.Flagged);
        }

        public bool IsCovered(int i, int j)
        {
            return _board[i - 1, j - 1].HasFlag(Field.Covered);
        }

        public bool IsNumber(int i, int j)
        {
            return !_board[i - 1, j - 1].HasFlag(Field.Mine);
        }

        private void TraverseNeighbors(int i, int j, bool isFirst = false)
        {
            if ((i < 0 || _board.GetLength(0) <= i) || (j < 0 || _board.GetLength(1) <= j))
                return;
            if (!_board[i, j].HasFlag(Field.Covered) && !isFirst)
                return;

            if (_board[i, j].HasFlag(Field.Covered))
                _board[i, j] = _board[i, j].ClearFlag(Field.Covered);

            if (_board[i, j].HasFlag(Field.Zero))
            {
                TraverseNeighbors(i - 1, j - 1);
                TraverseNeighbors(i - 1, j);
                TraverseNeighbors(i - 1, j + 1);
                TraverseNeighbors(i, j - 1);
                TraverseNeighbors(i, j + 1);
                TraverseNeighbors(i + 1, j - 1);
                TraverseNeighbors(i + 1, j);
                TraverseNeighbors(i + 1, j + 1);
            }

        }

        public Field Get(int i, int j)
        {
            return _board[i - 1, j - 1];
        }

        public bool IsRunning()
        {
            return !_steppedOnMine;
        }
    }
}
