using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.UI.Interfaces
{
    interface IBestScoresView
    {
        List<Tuple<string, int>> Scores
        {
            set;
        }
        int BestScore
        {
            set;
        }
    }
}
