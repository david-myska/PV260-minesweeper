using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class RandInt : IRandom
    {
        private Random _generator = new Random();
        public int NextInt()
        {
            return _generator.Next();
        }
    }
}
