using Games.MinesweeperClasses.Enums;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Games.MinesweeperClasses
{
    public class MinesweeperGrid
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public int NumBombs { get; set; }

        public Dictionary<(int xPos, int yPos), MinesweeperSquare> Squares { get; private set; }

        // Grid is won when there are no non-bomb squares that are unrevealed.
        public bool IsWon => !Squares.Where(x => !x.Value.IsBomb && x.Value.Status != MinesweeperSquareStatus.Revealed).Any();

        // Grid is lost when there are revealed bombs.
        public bool IsLost => Squares.Where(x => x.Value.IsBomb && x.Value.Status == MinesweeperSquareStatus.Revealed).Any();

        public bool IsEmpty => !Squares.Where(x => x.Value.Status == MinesweeperSquareStatus.Revealed).Any();

        public MinesweeperGrid(int width, int height, int numBombs = 0, int clickedX = 0, int clickedY = 0)
        {
            Width = width;
            Height = height;
            NumBombs = numBombs;
            Squares = AddBombsToGrid(CreateGrid(width, height), numBombs, clickedX, clickedY);
        }

        private Dictionary<(int, int), MinesweeperSquare> CreateGrid(int width, int height)
        {
            var squares = new Dictionary<(int, int), MinesweeperSquare>();
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    squares.Add((x, y), new MinesweeperSquare());
                }
            }
            return squares;
        }

        public Dictionary<(int, int), MinesweeperSquare> AddBombsToGrid(Dictionary<(int, int), MinesweeperSquare> squares, int numBombs, int clickedX, int clickedY)
        {
            // TODO: Make this not populate adjacent squares to first click with bombs.
            squares
                .OrderBy(x => Guid.NewGuid())
                .Where(x => x.Key != (clickedX, clickedY))
                .Take(numBombs)
                .ToList()
                .ForEach(x => x.Value.IsBomb = true);

            return squares;
        }

        public MinesweeperSquare GetSquare(int xPos, int yPos)
        {
            try
            {
                return Squares[(xPos, yPos)];
            }
            catch
            {
                return null;
            }
        }

        public MarkupString GetSquareMarkupString(int xPos, int yPos)
        {
            var square = GetSquare(xPos, yPos) ?? throw new ArgumentException("The given xPos and yPos do not have a valid square");

            return square.Status switch
            {
                MinesweeperSquareStatus.Revealed => square.IsBomb ? (MarkupString)"<i class=\"oi oi-warning\"></i>" : (MarkupString)GetNumOfAdjacentBombs(xPos, yPos).ToString(),
                MinesweeperSquareStatus.Flagged => (MarkupString)"<i class=\"oi oi-flag\"></i>",
                MinesweeperSquareStatus.Empty => (MarkupString)"",
                _ => throw new ArgumentException("This square does not have a valid status.")
            };
        }

        private int GetNumOfAdjacentBombs(int xPos, int yPos)
        {
            var numAdjacentBombs = 0;
            for (int xDelta = -1; xDelta <= 1; xDelta++)
            {
                for (int yDelta = -1; yDelta <= 1; yDelta++)
                {
                    if (xDelta == 0 && yDelta == 0) continue;       // Don't get the current square.

                    var adjacentSquare = GetSquare(xPos + xDelta, yPos + yDelta);
                    if (adjacentSquare != null && adjacentSquare.IsBomb) numAdjacentBombs++;
                }
            }
            return numAdjacentBombs;
        }

        // TODO: Finish this function
        // Clears all adjacent squares with zeros in them.
        public void ClearAllSafeSquares(int clickedXPos, int clickedYPos)
        {

        }

        public void ClearGrid()
        {
            Squares = CreateGrid(Width, Height);
        }

        public void RevealAllBombs()
        {
            Squares.Where(x => x.Value.IsBomb).Select(x => x.Value).ToList().ForEach(x => x.Reveal());
        }
    }
}
