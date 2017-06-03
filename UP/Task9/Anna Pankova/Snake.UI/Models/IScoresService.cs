using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.UI
{

    public interface IScoresService
    {
        List<Tuple<string, int>> Scores
        {
            get;
        }

       int BestScore
        {
            get;
        }
        void AddScore(string name, int score);
    }
}
