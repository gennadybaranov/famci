using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeUP
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            this.Hide();
            SnakeGame gameField = new SnakeGame();
            gameField.Show();
        }
      
        private void StartMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void BestScores_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            BestScores bestScores = new BestScores();
            bestScores.Show();
        }
    }
}
