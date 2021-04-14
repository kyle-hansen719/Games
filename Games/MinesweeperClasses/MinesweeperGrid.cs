using Games.MinesweeperClasses.Enums;
using Microsoft.AspNetCore.Components;
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

        public List<MinesweeperSquare> Squares { get; private set; }

        // Grid is won when there are no non-bomb squares that are unrevealed.
        public bool IsWon => !Squares.Where(x => !x.IsBomb && x.Status != MinesweeperSquareStatus.Revealed).Any();

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
            var index = 0;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    Squares.Add(new MinesweeperSquare(x, y, index));
                    index++;
                }
            }
            return Squares;
        }

        public List<MinesweeperSquare> AddBombsToGrid(List<MinesweeperSquare> squares, int numBombs, int clickedX, int clickedY)
        {
            var clickedSqaures = GetAdjacentSquares(clickedX, clickedY);
            clickedSqaures.Add(GetSquare(clickedX, clickedY));

            squares
                .OrderBy(x => Guid.NewGuid())
                .Where(x => !clickedSqaures.Contains(x))
                .Take(numBombs)
                .ToList()
                .ForEach(x => x.IsBomb = true);

            return squares;
        }

        //use a dictionary for this
        public MinesweeperSquare GetSquare(int xPos, int yPos)
        {
            return Squares.Where(x => x.xPos == xPos && x.yPos == yPos).FirstOrDefault();
        }

        public MarkupString GetSquareMarkupString(MinesweeperSquare square)
        {
            switch (square.Status)
            {
                case MinesweeperSquareStatus.Revealed:
                    if (square.IsBomb)
                    {
                        return (MarkupString)"<i class=\"oi oi-warning\"></i>";
                    }
                    return (MarkupString)GetNumOfAdjacentBombs(square.xPos, square.yPos).ToString();
                case MinesweeperSquareStatus.Flagged:
                    return (MarkupString)"<i class=\"oi oi-flag\"></i>";
                default:
                    return (MarkupString)"";
            }
        }

        //public void ChangeSquareStatus(MinesweeperSquare square)
        //{
        //    square.Reveal();

        //    var adjacentSquares = GetAdjacentSquares(square.xPos, square.yPos);

        //    if (adjacentSquares.Where(x => x.IsBomb).Count() == 0)
        //    {
        //        //ChangeSquareStatus(adjacentSquares[0]);

        //        adjacentSquares.ForEach(x => ChangeSquareStatus(x));
        //    }
        //}

        private int GetNumOfAdjacentBombs(int xPos, int yPos)
        {
            return GetAdjacentSquares(xPos, yPos)
                .Where(x => x.IsBomb)
                .Count();
        }

        private List<MinesweeperSquare> GetAdjacentSquares(int xPos, int yPos)
        {
            return Squares
                .Where(x => (x.xPos == xPos + 1 && x.yPos == yPos)
                    || (x.xPos == xPos + 1 && x.yPos == yPos + 1)
                    || (x.xPos == xPos + 1 && x.yPos == yPos - 1)
                    || (x.xPos == xPos && x.yPos == yPos + 1)
                    || (x.xPos == xPos && x.yPos == yPos - 1)
                    || (x.xPos == xPos - 1 && x.yPos == yPos)
                    || (x.xPos == xPos - 1 && x.yPos == yPos + 1)
                    || (x.xPos == xPos - 1 && x.yPos == yPos - 1))
                .ToList();
        }

        public void ClearGrid()
        {
            Squares = Squares.Select(x => new MinesweeperSquare(x.xPos, x.yPos, x.SquaresIndex)).ToList();
        }
    }
}
