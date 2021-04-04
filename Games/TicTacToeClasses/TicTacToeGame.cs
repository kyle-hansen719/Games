using System;

namespace Games.TicTacToeClasses
{
    public class TicTacToeGame
    {
        public Grid grid { get; set; } = new Grid();

        public bool isCrossTurn { get; set; } = true;

        public TicTocToeTeam Winner { get; set; } = TicTocToeTeam.None;

        public void ChangeSquareStatus(int squareXPos, int squareYPos)
        {
            if (grid.isWonByCircle || grid.isWonByCross) return;

            var clickedSquare = grid.GetSquare(squareXPos, squareYPos);

            if (clickedSquare.Status != SquareStatus.Empty) return;

            clickedSquare.Status = (SquareStatus)Convert.ToInt32(isCrossTurn);

            if (grid.isWonByCircle)
            {
                Winner = TicTocToeTeam.Circle;
                return;
            }
            else if (grid.isWonByCross)
            {
                Winner = TicTocToeTeam.Cross;
                return;
            }

            isCrossTurn = !isCrossTurn;
        }
    }
}
