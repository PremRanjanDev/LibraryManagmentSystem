using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library_Management
{
    partial class AboutBoxMember : Form
    {
        string memberID = frmMain.detailMemberID;
        public AboutBoxMember()
        {
            InitializeComponent();
        }


        private void AboutBoxMember_Load(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=.;Initial Catalog=Library;Integrated Security=True";
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * From Member where Member_ID = @memberID", cn);
            cmd.Parameters.AddWithValue("memberID", memberID);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            lblMemberID.Text = dr[0].ToString();
            lblMemberName.Text = dr[1].ToString();
            lblMemberRollNo.Text = dr[2].ToString();
            lblMemberBranch.Text = dr[3].ToString();
            lblMemberSemester.Text = dr[4].ToString();
            lblMemberGender.Text = dr[5].ToString();
            lblMemberPhone.Text = dr[6].ToString();
            lblMemberEmail.Text = dr[7].ToString();
            lblMemberAddress.Text = dr[8].ToString();
            lblMemberIssue.Text = dr[9].ToString();
            cn.Close();
            btnOK.Focus();
        }

        private void btnOK_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void lblMemberIssue_Click(object sender, EventArgs e)
        {
            if (lblMemberIssue.Text == "-")
            {
                MessageBox.Show("Information Not Available");
            }
            else
            {
                frmMain.detailBookID = lblMemberIssue.Text;
                AboutBoxBook DetailBook = new AboutBoxBook();
                DetailBook.Show();
                this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
