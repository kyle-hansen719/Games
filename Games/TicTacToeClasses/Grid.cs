using System.Collections.Generic;
using System.Linq;

namespace Games.TicTacToeClasses
{
    public class Grid
    {
        public List<Square> Squares { get; set; }

        public Grid()
        {
            // Creating grid of sqaures.
            Squares = new List<Square>();

            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    Squares.Add(new Square(x, y));
                }
            }
        }

        public bool isWonByCircle => GetWinner() == TicTacToeTeam.Circle;

        public bool isWonByCross => GetWinner() == TicTacToeTeam.Cross;

        public bool isWon => isWonByCircle || isWonByCross;

        public bool isEmpty => Squares.Where(x => x.Status == SquareStatus.Empty).Count() == Squares.Count();

        public List<Square> PossibleMoves => Squares.Where(x => x.Status == SquareStatus.Empty).ToList();

        // Fix This
        public TicTacToeTeam GetWinner()
        {
            // Checking columns for wins.
            for (var i = 0; i < 3; i++)
            {
                if (Squares.Where(x => x.xPos == i && x.Status == SquareStatus.Circle).Count() == Squares.Where(x => x.xPos == i).Count())
                {
                    return TicTacToeTeam.Circle;
                }
                else if (Squares.Where(x => x.xPos == i && x.Status == SquareStatus.Cross).Count() == Squares.Where(x => x.xPos == i).Count())
                {
                    return TicTacToeTeam.Cross;
                }
            }

            // Checking rows for wins.
            for (var i = 0; i < 3; i++)
            {
                if (Squares.Where(x => x.yPos == i && x.Status == SquareStatus.Circle).Count() == Squares.Where(x => x.xPos == i).Count())
                {
                    return TicTacToeTeam.Circle;
                }
                else if (Squares.Where(x => x.yPos == i && x.Status == SquareStatus.Cross).Count() == Squares.Where(x => x.xPos == i).Count())
                {
                    return TicTacToeTeam.Cross;
                }
            }

            // Checking diagonals for wins.
            if (GetSquare(0, 0).Status == SquareStatus.Circle && GetSquare(1, 1).Status == SquareStatus.Circle && GetSquare(2, 2).Status == SquareStatus.Circle)
            {
                return TicTacToeTeam.Circle;
            }
            else if (GetSquare(0, 0).Status == SquareStatus.Cross && GetSquare(1, 1).Status == SquareStatus.Cross && GetSquare(2, 2).Status == SquareStatus.Cross)
            {
                return TicTacToeTeam.Cross;
            }

            if (GetSquare(0, 2).Status == SquareStatus.Circle && GetSquare(1, 1).Status == SquareStatus.Circle && GetSquare(2, 0).Status == SquareStatus.Circle)
            {
                return TicTacToeTeam.Circle;
            }
            else if (GetSquare(0, 2).Status == SquareStatus.Cross && GetSquare(1, 1).Status == SquareStatus.Cross && GetSquare(2, 0).Status == SquareStatus.Cross)
            {
                return TicTacToeTeam.Cross;
            }

            return TicTacToeTeam.None;
        }

        public Square GetSquare(int xPosition, int yPosition)
        {
            return Squares.Where(x => x.xPos == xPosition && x.yPos == yPosition).FirstOrDefault();
        }
    }
}
