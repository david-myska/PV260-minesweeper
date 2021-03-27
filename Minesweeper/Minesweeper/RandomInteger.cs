using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class RandomInteger : IRandomInteger
    {
        private Random _generator = new Random();
        public int NextInt(int low, int high)
        {
            return _generator.Next(low, high + 1);
        }
    }
}
