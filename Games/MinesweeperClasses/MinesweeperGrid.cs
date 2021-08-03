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
                    squares.Add((x, y), new MinesweeperSquare(x, y));
                }
            }
            return squares;
        }

        public Dictionary<(int, int), MinesweeperSquare> AddBombsToGrid(Dictionary<(int, int), MinesweeperSquare> squares, int numBombs, int clickedX, int clickedY)
        {
            var adjacentBombCoords = GetAdjacentSquares(clickedX, clickedY).Select(x => (x.XPos, x.YPos));

            squares
                .OrderBy(x => Guid.NewGuid())
                .Where(x => !adjacentBombCoords.Contains(x.Key) && x.Key != (clickedX, clickedY))
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

        // TODO: Make this private
        public int GetNumOfAdjacentBombs(int xPos, int yPos)
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

        // Clears all adjacent squares with zeros in them.
        public void ClearAllSafeSquares(int clickedXPos, int clickedYPos)
        {
            var adjacentSqaures = GetAdjacentSquares(clickedXPos, clickedYPos);

            foreach (var square in adjacentSqaures)
            {
                if (square.Status == MinesweeperSquareStatus.Revealed) continue;

                square.Reveal();

                if (GetNumOfAdjacentBombs(square.XPos, square.YPos) == 0)
                {
                    ClearAllSafeSquares(square.XPos, square.YPos);
                }
            }
        }

        private List<MinesweeperSquare> GetAdjacentSquares(int xPos, int yPos)
        {
            var adjacentSqaures = new List<MinesweeperSquare>();
            for (int xDelta = -1; xDelta <= 1; xDelta++)
            {
                for (int yDelta = -1; yDelta <= 1; yDelta++)
                {
                    if (xDelta == 0 && yDelta == 0) continue;

                    var adjacentSquare = GetSquare(xPos + xDelta, yPos + yDelta);
                    if (adjacentSquare != null) adjacentSqaures.Add(adjacentSquare);
                }
            }
            return adjacentSqaures;
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
