using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library_Management
{
    public partial class frmLogin : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        public static string loginAs;
        string empUsername, adminPassword, empPassword;

        public frmLogin()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            try
            {
                cn.Open();
                cmd = new SqlCommand("Select Value From Setting where Variable = 'Admin Password'", cn);
                dr = cmd.ExecuteReader();
                dr.Read();
                adminPassword = dr[0].ToString();
                cn.Close();

                cn.Open();
                cmd = new SqlCommand("Select Value From Setting where Variable = 'Emp Username'", cn);
                dr = cmd.ExecuteReader();
                dr.Read();
                empUsername = dr[0].ToString();
                cn.Close();

                cn.Open();
                cmd = new SqlCommand("Select Value From Setting where Variable = 'Emp Password'", cn);
                dr = cmd.ExecuteReader();
                dr.Read();
                empPassword = dr[0].ToString();
                cn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection();
            cn.ConnectionString = "Data Source=.;Initial Catalog=Library;Integrated Security=True";
            LoadData();
            cmbLoginAs.Text = "Employee";
            txtUsername.Focus();
        }

        private void cmbLoginAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLoginAs.Text == "Administrator")
            {
                txtUsername.Text = "Administrator";
                txtUsername.Enabled = false;
            }
            else if (cmbLoginAs.Text == "Employee")
            {
                txtUsername.Text = "";
                txtUsername.Enabled = true;
            }
        }

        private void btnLoginClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStartWithoutLogin_Click(object sender, EventArgs e)
        {
            loginAs = "Public";
            this.Visible = false;
            frmMain form = new frmMain();
            form.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            if (cmbLoginAs.Text == "Administrator")
            {
                if (txtPassword.Text == "")
                {
                    errorProvider.SetError(txtPassword, "**Please Enter Password");
                }
                else
                {
                    if (txtPassword.Text == adminPassword)
                    {
                        loginAs = cmbLoginAs.Text;
                        this.Visible = false;
                        frmMain form = new frmMain();
                        form.Show();
                    }
                    else
                    {
                        lblInvalidMessage.Visible = true;
                        lblInvalidMessage.Text = "*~Invalid Administrator's Password~*";
                    }
                }
            }
            else if (cmbLoginAs.Text == "Employee")
            {
                if (txtUsername.Text == "")
                {
                    errorProvider.SetError(txtUsername, "**Please Enter Username");
                }
                else if (txtPassword.Text == "")
                {
                    errorProvider.SetError(txtPassword, "**Please Enter Password");
                }
                else
                {
                    if (empUsername == txtUsername.Text && empPassword == txtPassword.Text)
                    {
                        loginAs = cmbLoginAs.Text;
                        this.Visible = false;
                        frmMain form = new frmMain();
                        form.Show();
                    }
                    else
                    {
                        lblInvalidMessage.Visible = true;
                        lblInvalidMessage.Text = "*~Invalid Employee's Username Or Password~*";
                    }
                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                object se = new object();
                EventArgs ev = new EventArgs();
                btnLogin_Click(se, ev);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                object se = new object();
                EventArgs ev = new EventArgs();
                btnLogin_Click(se, ev);
            }
        }

        private void cmbLoginAs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                object se = new object();
                EventArgs ev = new EventArgs();
                btnLogin_Click(se, ev);
            }
        }
    }
}
