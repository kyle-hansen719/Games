using Games.MinesweeperClasses.Enums;

namespace Games.MinesweeperClasses
{
    public class MinesweeperSquare
    {
        public MinesweeperSquareStatus Status { get; private set; } = MinesweeperSquareStatus.Empty;

        public bool IsBomb { get; set; } = false;

        public int XPos { get; set; }

        public int YPos { get; set; }

        public MinesweeperSquare(int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
        }

        public void Reveal()
        {
            Status = MinesweeperSquareStatus.Revealed;
        }

        public void Flag()
        {
            // If already flagged, set to empty.
            Status = Status == MinesweeperSquareStatus.Flagged ? MinesweeperSquareStatus.Empty : MinesweeperSquareStatus.Flagged;
        }
    }
}
