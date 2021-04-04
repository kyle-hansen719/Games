using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.TicTacToeClasses
{
    public class Square
    {
        public SquareStatus Status { get; set; }

        public int xPos { get; set; }

        public int yPos { get; set; }

        public Square(int xPosition, int yPosition)
        {
            Status = SquareStatus.Empty;
            xPos = xPosition;
            yPos = yPosition;
        }

        public string GetStatusHtml()
        {
            switch (Status)
            {
                case SquareStatus.Circle:
                    return "O";
                case SquareStatus.Cross:
                    return "X";
                default:
                    return "";
            }
        }
    }
}
