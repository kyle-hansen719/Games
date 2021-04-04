using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.TicTacToeClasses
{
    public class Grid
    {
        public List<Square> Squares { get; set; }

        public Grid()
        {
            Squares = new List<Square>();

            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    Squares.Add(new Square(x, y));
                }
            }
        }

        public bool isWonByCircle => GetWinner() == TicTocToeTeam.Circle;

        public bool isWonByCross => GetWinner() == TicTocToeTeam.Cross;

        public bool isWon => isWonByCircle || isWonByCross;

        public bool isEmpty => Squares.Where(x => x.Status == SquareStatus.Empty).Count() == Squares.Count();

        private TicTocToeTeam GetWinner()
        {
            // Checking columns for wins.
            for (var i = 0; i < 3; i++)
            {
                if (!Squares.Where(x => x.xPos == i && x.Status == SquareStatus.Empty).Any())
                {
                    return (TicTocToeTeam)Squares.Where(x => x.xPos == i).First().Status;
                }
            }

            // Checking rows for wins.
            for (var i = 0; i < 3; i++)
            {
                if (!Squares.Where(x => x.yPos == i && x.Status == SquareStatus.Empty).Any())
                {
                    return (TicTocToeTeam)Squares.Where(x => x.yPos == i).First().Status;
                }
            }

            // Checking diagonals for wins.
            if (GetSquare(0, 0).Status != SquareStatus.Empty && GetSquare(1, 1).Status != SquareStatus.Empty && GetSquare(2, 2).Status != SquareStatus.Empty)
            {
                return (TicTocToeTeam)GetSquare(1, 1).Status;
            }

            if (GetSquare(0, 2).Status != SquareStatus.Empty && GetSquare(1, 1).Status != SquareStatus.Empty && GetSquare(2, 0).Status != SquareStatus.Empty)
            {
                return (TicTocToeTeam)GetSquare(1, 1).Status;
            }

            return TicTocToeTeam.None;
        }

        public Square GetSquare(int xPosition, int yPosition)
        {
            return Squares.Where(x => x.xPos == xPosition && x.yPos == yPosition).FirstOrDefault();
        }
    }
}
