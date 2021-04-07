using System;

namespace Games.TicTacToeClasses
{
    public class TicTacToeGame
    {
        public Grid grid { get; set; } = new Grid();

        public AiOpponent Opponent { get; set; }

        public TicTacToeTeam Winner { get; set; } = TicTacToeTeam.None;
        
        public bool IsCrossTurn { get; set; }

        public TicTacToeGame(AiOpponent opponent, bool isCrossTurn)
        {
            Opponent = opponent;
            IsCrossTurn = isCrossTurn;
        }

        public void ExecuteTurn(int xPos, int yPos, bool playVsComputer)
        {
            if (ChangeSquareStatus(xPos, yPos) == -1) return;

            if (playVsComputer)
            {
                AiMove();
            }
        }

        public void AiMove()
        {
            var bestSquare = Opponent.GetBestMove(grid);
            if (bestSquare is null) return;
            ChangeSquareStatus(bestSquare.xPos, bestSquare.yPos);
        }

        //fix this so it returns something other than a status code
        private int ChangeSquareStatus(int squareXPos, int squareYPos)
        {
            if (grid.isWonByCircle || grid.isWonByCross) return -1;

            var clickedSquare = grid.GetSquare(squareXPos, squareYPos);

            if (clickedSquare.Status != SquareStatus.Empty) return -1;

            clickedSquare.Status = (SquareStatus)Convert.ToInt32(IsCrossTurn);

            Winner = grid.GetWinner();

            IsCrossTurn = !IsCrossTurn;

            return 1;
        }

        public TicTacToeTeam GetOppositeTeam(TicTacToeTeam team)
        {
            switch (team)
            {
                case TicTacToeTeam.Circle:
                    return TicTacToeTeam.Cross;
                case TicTacToeTeam.Cross:
                    return TicTacToeTeam.Circle;
                default:
                    return TicTacToeTeam.None;
            }
        }
    }
}
