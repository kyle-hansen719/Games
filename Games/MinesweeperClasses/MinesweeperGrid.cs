using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.MinesweeperClasses
{
    public class MinesweeperGrid
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public int NumBombs { get; set; }

        public List<MinesweeperSquare> Squares { get; set; }

        public MinesweeperGrid(int width, int height, int numBombs)
        {
            Width = width;
            Height = height;
            NumBombs = numBombs;

            // Creating grid of sqaures.
            Squares = new List<MinesweeperSquare>();

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    Squares.Add(new MinesweeperSquare(x, y));
                }
            }

            // Adding bombs to Squares.
            for (var i = 0; i < numBombs; i++)
            {
                var square = Squares.OrderBy(x => Guid.NewGuid()).First();
                if (!square.IsBomb)
                {
                    square.IsBomb = true;
                }
                else
                {
                    i--;
                }
            }
        }

        public MinesweeperSquare GetSquare(int xPos, int yPos)
        {
            return Squares.Where(x => x.xPos == xPos && x.yPos == yPos).FirstOrDefault();
        }

        public int GetNumOfAdjacentBombs(int xPos, int yPos)
        {
            return Squares
                .Where(x => x.IsBomb)
                .Where(x => (x.xPos == xPos + 1 && x.yPos == yPos)
                    || (x.xPos == xPos + 1 && x.yPos == yPos + 1)
                    || (x.xPos == xPos + 1 && x.yPos == yPos - 1)
                    || (x.xPos == xPos && x.yPos == yPos + 1) 
                    || (x.xPos == xPos && x.yPos == yPos - 1)
                    || (x.xPos == xPos - 1 && x.yPos == yPos)
                    || (x.xPos == xPos - 1 && x.yPos == yPos + 1)
                    || (x.xPos == xPos - 1 && x.yPos == yPos - 1))
                .Count();
        }
    }
}
