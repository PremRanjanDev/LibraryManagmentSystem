using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Library_Management
{
    public partial class frmWelcome : Form
    {
        public frmWelcome()
        {
            InitializeComponent();
        }
        private void frmWelcome_Load(object sender, EventArgs e)
        {

        }
        private void timer_Tick_1(object sender, EventArgs e)
        {
            if (progressBar.Value < progressBar.Maximum)
            {
                progressBar.Value = progressBar.Value + 100;
            }
            else if (progressBar.Value >= progressBar.Maximum)
            {
                timer.Enabled = false;
                
                //MessageBox.Show("Welcome To Library Manegement System...!", "Mesage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                frmLogin login = new frmLogin();
                login.Show();
                this.Visible = false;
            }
        }

    }
}
