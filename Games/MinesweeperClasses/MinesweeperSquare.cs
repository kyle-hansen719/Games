using Games.MinesweeperClasses.Enums;

namespace Games.MinesweeperClasses
{
    public class MinesweeperSquare
    {
        public int Id { get; }
        public int xPos { get; }

        public int yPos { get; }

        public MinesweeperSquareStatus Status { get; private set; } = MinesweeperSquareStatus.Empty;

        public bool IsBomb { get; set; } = false;

        public int NumAdjacentBombs { get; set; }

        public MinesweeperSquare(int xPosition, int yPosition, int id)
        {
            xPos = xPosition;
            yPos = yPosition;
            Id = id;
        }

        public void Reveal()
        {
            Status = MinesweeperSquareStatus.Revealed;
        }

        public void Flag()
        {
            Status = MinesweeperSquareStatus.Flagged;
        }
    }
}
