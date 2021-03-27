using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    static class FieldExtension
    {
        public static Field SetFlag(this Field field, Field flag)
        {
            field |= flag;
            return field;
        }
        public static Field ClearFlag(this Field field, Field flag)
        {
            field &= ~flag;
            return field;
        }
    }
}
