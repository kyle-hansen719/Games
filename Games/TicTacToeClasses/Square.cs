﻿
namespace Games.TicTacToeClasses
{
    public class Square
    {
        public SquareStatus Status { get; set; } = SquareStatus.Empty;

        public int xPos { get; set; }

        public int yPos { get; set; }

        public Square(int xPosition, int yPosition)
        {
            xPos = xPosition;
            yPos = yPosition;
        }

        public string GetStatusHtml()
        {
            switch (Status)
            {
                case SquareStatus.Circle:
                    return "oi oi-target red";
                case SquareStatus.Cross:
                    return "oi oi-x blue";
                default:
                    return "no-icon";
            }
        }
    }
}
