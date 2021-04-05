using System;

namespace Games.TicTacToeClasses
{
    public class TicTacToeGame
    {
        public Grid grid { get; set; } = new Grid();

        public bool isCrossTurn { get; set; } = true;

        public TicTacToeTeam Winner { get; set; } = TicTacToeTeam.None;


        //fix this so it returns something other than a status code
        public int ChangeSquareStatus(int squareXPos, int squareYPos)
        {
            if (grid.isWonByCircle || grid.isWonByCross) return -1;

            var clickedSquare = grid.GetSquare(squareXPos, squareYPos);

            if (clickedSquare.Status != SquareStatus.Empty) return -1;

            clickedSquare.Status = (SquareStatus)Convert.ToInt32(isCrossTurn);

            Winner = grid.GetWinner();
            //if (grid.isWonByCircle)
            //{
            //    Winner = TicTacToeTeam.Circle;
            //    return 1;
            //}
            //else if (grid.isWonByCross)
            //{
            //    Winner = TicTacToeTeam.Cross;
            //    return 1;
            //}

            isCrossTurn = !isCrossTurn;

            return 1;
        }
    }
}
