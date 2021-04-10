using Games.MinesweeperClasses.Enums;
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

        // Grid is won when there are no non-bomb squares that are unrevealed.
        public bool IsWon => Squares.Where(x => !x.IsBomb && x.Status == MinesweeperSquareStatus.Empty).Any();

        // Grid is lost when there are revealed bombs.
        public bool IsLost => Squares.Where(x => x.IsBomb && x.Status == MinesweeperSquareStatus.Revealed).Any();

        public bool IsEmpty => !Squares.Where(x => x.Status == MinesweeperSquareStatus.Revealed).Any();

        public MinesweeperGrid(int width, int height, int numBombs, int clickedX, int clickedY)
        {
            Width = width;
            Height = height;
            NumBombs = numBombs;
            Squares = AddBombsToGrid(CreateGrid(width, height), numBombs, clickedX, clickedY);
        }

        private List<MinesweeperSquare> CreateGrid(int width, int height)
        {
            Squares = new List<MinesweeperSquare>();
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    Squares.Add(new MinesweeperSquare(x, y));
                }
            }
            return Squares;
        }

        //STILL ADDS BOMBS TO CLICKED SQUARES
        private List<MinesweeperSquare> AddBombsToGrid(List<MinesweeperSquare> squares, int numBombs, int clickedX, int clickedY)
        {
            for (var i = 0; i < numBombs; i++)
            {
                var square = squares
                    .OrderBy(x => Guid.NewGuid())
                    .Where(x => !x.IsBomb)
                    .FirstOrDefault();

                if (square is null) return squares;

                square.IsBomb = true;
            }
            return squares;
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

        public void ClearGrid()
        {
            Squares = Squares.Select(x => new MinesweeperSquare(x.xPos, x.yPos)).ToList();
        }
    }
}
