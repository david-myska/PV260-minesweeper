using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public interface IGameBoardCreator
    {
        Field[,] GenerateGameBoard(int dimX, int dimY);
    }
}
