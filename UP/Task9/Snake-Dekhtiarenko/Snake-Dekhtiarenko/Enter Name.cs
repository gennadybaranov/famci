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
            //Image myimage = new Bitmap(@"D:\Screenshots\PhotoFunia-1496587468.jpg");
            //this.BackgroundImage = myimage;
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Button button1 = new Button();
            //button1.BackColor = TransparencyKey;
            Form1 dialog = new Form1();
            dialog.ShowDialog();
        }

        private void nameEnter_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
