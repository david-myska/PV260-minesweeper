using System;

namespace Minesweeper
{
    public interface IGameBoard
    {
        Field Get(int i, int j);
        void Choose(int i, int j);
        bool IsMine(int i, int j);
        bool IsFlagged(int i, int j);
        bool IsCovered(int i, int j);
        bool IsNumber(int i, int j);

        bool IsRunning();
    }
}
