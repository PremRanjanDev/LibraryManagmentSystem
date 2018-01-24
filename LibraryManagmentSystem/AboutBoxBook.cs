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
    partial class AboutBoxBook : Form
    {
        string bookID = frmMain.detailBookID;
        public AboutBoxBook()
        {
            InitializeComponent();
        }

        private void AboutBoxBook_Load(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=.;Initial Catalog=Library;Integrated Security=True";
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * From Book where Book_ID = @bookID", cn);
            cmd.Parameters.AddWithValue("bookID", bookID);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            lblBookID.Text = dr[0].ToString();
            lblBookName.Text = dr[1].ToString();
            lblBookCategory.Text = dr[2].ToString();
            lblBookEdition.Text = dr[3].ToString();
            lblBookAuthor.Text = dr[4].ToString();
            lblBookPublisher.Text = dr[5].ToString();
            lblBookPrice.Text = dr[6].ToString();
            lblBookIssue.Text = dr[7].ToString();
            cn.Close();
            btnOK.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblBookIssue_Click(object sender, EventArgs e)
        {
            if (lblBookIssue.Text == "-")
            {
                MessageBox.Show("Information Not Available");
            }
            else
            {
                frmMain.detailMemberID = lblBookIssue.Text;
                AboutBoxMember DetailMember = new AboutBoxMember();
                DetailMember.Show();
                this.Close();
            }
        }
    }
}
