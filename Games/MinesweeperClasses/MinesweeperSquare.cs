using Games.MinesweeperClasses.Enums;

namespace Games.MinesweeperClasses
{
    public class MinesweeperSquare
    {
        public MinesweeperSquareStatus Status { get; private set; } = MinesweeperSquareStatus.Empty;

        public bool IsBomb { get; set; } = false;

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
