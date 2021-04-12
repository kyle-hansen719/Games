﻿using Games.MinesweeperClasses.Enums;
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
        public bool IsWon => !Squares.Where(x => !x.IsBomb && x.Status == MinesweeperSquareStatus.Empty).Any();

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
            var newId = 0;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    Squares.Add(new MinesweeperSquare(x, y, newId));
                    newId++;
                }
            }
            return Squares;
        }

        private List<MinesweeperSquare> AddBombsToGrid(List<MinesweeperSquare> squares, int numBombs, int clickedX, int clickedY)
        {
            var clickedSqaures = GetAdjacentSquares(clickedX, clickedY);
            clickedSqaures.Add(GetSquare(clickedX, clickedY));

            //Might want to optimize this (can take a while when there are lots of bombs)
            for (var i = 0; i < numBombs; i++)
            {
                var square = squares
                    .OrderBy(x => Guid.NewGuid())
                    .Where(x => !x.IsBomb)
                    .Where(x => !clickedSqaures.Contains(x))
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

        public MinesweeperSquare GetSquareById(int id)
        {
            return Squares.Where(x => x.Id == id).FirstOrDefault();
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
            Squares = Squares.Select(x => new MinesweeperSquare(x.xPos, x.yPos, x.Id)).ToList();
        }
    }
}
