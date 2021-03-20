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

        public Field Get(int i, int j)
        {
            return _board[i - 1, j - 1];
        }
    }
}
