using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.IO;


namespace SnakeUP
{
    public partial class BestScores : Form
    {
        public List<Player> players = new List<Player>();
        public BestScores()
        {
            InitializeComponent();

            using (StreamReader reader = File.OpenText("Score.txt"))
            {
                string score = null;
                string date = null;
                while(true)
                {
                    Player temp = new Player();

                    score = reader.ReadLine();
                    date = reader.ReadLine();
                    if (score == null)
                    {
                        break;
                    }
                    temp.Score = Convert.ToInt32(score);
                    temp.Date = date;
                    players.Add(temp);
                }
            }

            for (int i = 0; i < players.Count; i++)
            {
                for (int j = players.Count - 1; j > i; j--)
                {
                    if (players[j - 1].Score < players[j].Score)
                    {
                        var x = players[j - 1];
                        players[j - 1] = players[j];
                        players[j] = x;
                    }
                }
            }

            for(int i = 0; i < players.Count; ++i)
            {
                ListViewItem lvi = new ListViewItem(players[i].Score.ToString());
                lvi.SubItems.Add(players[i].Date);
                listView1.Items.Add(lvi);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartMenu startMenu = new StartMenu();
            startMenu.Show(); 
        }
    }
}
