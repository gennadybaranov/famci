using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Dekhtiarenko
{
    
    public partial class EnterName : Form
    {
        public EnterName()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 dialog = new Form1();
            dialog.Owner = this;
            dialog.name = this.nameEnter.Text;
            this.Visible = false;
            dialog.ShowDialog();
        }

        private void nameEnter_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
