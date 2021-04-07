using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.TicTacToeClasses
{
    public class MinimaxResult
    {
        public int NumOutcomesAnalyzed { get; set; }

        public Square BestMove { get; set; }

        public Square PredictedMove { get; set; }
    }
}
