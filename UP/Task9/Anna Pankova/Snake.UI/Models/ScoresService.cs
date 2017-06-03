using Snake.UI.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.UI
{
    class ScoresService : IScoresService
    {
        public List<Tuple<string, int>> Scores
        {
            get;
            private set;
        }

        public int BestScore
        {
            get
            {
                return Scores[0].Item2;
            }
        }

        private readonly string path = Settings.Default.fileName;

        public void AddScore(string name, int score)
        {
            if (name == "")
            {
                name = "new player";
            }

            Scores.Add(new Tuple<string, int>(name, score));

            Scores.Sort(Comparer<Tuple<string, int>>.Create((a, b) =>
            {
                return b.Item2 - a.Item2;
            }));
            while (Scores.Count > Settings.Default.numOfScores)
            {
                Scores.RemoveAt(Scores.Count - 1);
            }
            Save();
        }

        public ScoresService()
        {
            Scores = new List<Tuple<string, int>>() {
                new Tuple<string, int>( "kolya", 1),
                new Tuple<string, int>( "petya", 3),
                new Tuple<string, int>( "sasha", 2)
           };
            Load();
        }
        void Save()
        {
            string text = "";

            foreach (var score in Scores)
            {
                text += score.Item2 + " " + score.Item1 + "\n";
            }

            File.WriteAllText(path, text);
        }
        void Load()
        {
            Scores.Clear();
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    Scores.Add(new Tuple<string, int>(
                        line.Split(new char[] { ' ' }, 2)[1], Int32.Parse(line.Split(' ')[0])
                    ));
                }
            }
        }
    }
}
