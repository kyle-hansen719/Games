using Games.MinesweeperClasses.Enums;

namespace Games.MinesweeperClasses
{
    public class MinesweeperSquare
    {
        public int SquaresIndex { get; }

        public int xPos { get; }

        public int yPos { get; }

        public MinesweeperSquareStatus Status { get; private set; } = MinesweeperSquareStatus.Empty;

        public bool IsBomb { get; set; } = false;

        public MinesweeperSquare(int xPosition, int yPosition, int index)
        {
            xPos = xPosition;
            yPos = yPosition;
            SquaresIndex = index;
        }

        public void Reveal()
        {
            Status = MinesweeperSquareStatus.Revealed;
        }

        public void Flag()
        {
            // If already flagged, set to empty.
            if (Status == MinesweeperSquareStatus.Flagged)
            {
                Status = MinesweeperSquareStatus.Empty;
                return;
            }

            Status = MinesweeperSquareStatus.Flagged;
        }
    }
}
