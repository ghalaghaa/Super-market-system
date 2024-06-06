using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SuperMarket_G5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
                progressBar1.Value += 20;
                Thread.Sleep(600);
           

            if (progressBar1.Value == 100)
            {
                Login frm = new Login();
                frm.Show();
                this.Hide();
                timer1.Stop();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
