using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Diagnostics;

namespace IS_naloga_1.Game_of_Life
{
    internal class Cell
    {
        public bool IsAlive { get; set; } = false;
        public int NumberOfNeighbours { get; set; } = 0;
    }
}
