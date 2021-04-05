using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.TicTacToeClasses
{
    public class PossibleMove
    {
        public Square Square { get; set; }

        public TicTacToeTeam ResultingTeam { get; set; }
    }
}
