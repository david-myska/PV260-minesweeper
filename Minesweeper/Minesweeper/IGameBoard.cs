using System;

namespace Minesweeper
{
    public interface IGameBoard
    {
        Field Get(int i, int j);
    }
}
