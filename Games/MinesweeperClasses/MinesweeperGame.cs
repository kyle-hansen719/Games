using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.MinesweeperClasses
{
    public class MinesweeperGame
    {
        public MinesweeperGrid Grid { get; set; }

        public MinesweeperGame(int width, int height, int numBombs)
        {
            Grid = new MinesweeperGrid(width, height, numBombs);
        }
    }
}
