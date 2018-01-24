using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace Library_Management
{
    public partial class frmMain : Form
    {
        SqlConnection cn;           // Globle Variable used for made SQL Connection.
        SqlCommand cmd;             // Globle Variable used for made SQL Command.    
        SqlDataAdapter da;          // Globle Variable used for made SQL Data Adapter.
        SqlCommandBuilder bld;      // GLoble Variable used for made SQL Command Builder.
        DataSet ds;                 // Globle Variable used for made Data Set.
        SqlDataReader dr;           // Globle Variable used for made SQL Data Reader.

        public static string detailBookID, detailMemberID;      // Globle, Public and Static Variable to be access out of class. 
        string loginAs = frmLogin.loginAs;
        int maxDays;
        double fineCharge;

        public frmMain() // Constructor Of this (frmMain) lass.
        {
            InitializeComponent();  // To initialize all component used in.
        }
        private void frmMain_Load(object sender, EventArgs e)   // Loading Of the Form (frmMain).
        {
            cn = new SqlConnection();
            cn.ConnectionString = "Data Source=.;Initial Catalog=Library;Integrated Security=True";
            try
            {
                LoadAllData();
                LoadSetting();
            }

            catch (SqlException)
            {
                cn.Close();
                DialogResult result;
                string path = "C:\\Users\\ranjan\\Documents\\Visual Studio 2008\\Projects\\Library Management\\Library Management\\Library Management\\sql DataBase commands.txt";
                
                result = MessageBox.Show("Database Missing!! \nDatabase Either Not Created Properly Or Not Connected. \nDo You Want To Continue Without Database??", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0, path, HelpNavigator.Topic, "Rectangle");
                string res1 = HelpNavigator.Topic.ToString();
                string res2 = result.ToString();
                if (result == DialogResult.No)
                {
                    Application.Exit();
                }
                
                else if (result.Equals(res1))
                {
                    MessageBox.Show("");
                }
            }
            
            ClearAll();
            SetPermission();
        }
        private void timer_Tick(object sender, EventArgs e) // Timer.
        {
            dateTimePicker.Value = DateTime.Now;
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }
        public void SetPermission()
        {
            if (loginAs == "Public")
            {
                btnSaveMember.Enabled = false;
                btnSaveBook.Enabled = false;
                btnSettingChange.Enabled = false;
                btnSettingDefault.Enabled = false;
                btnIssueCheck.Enabled = false;
                btnReturnStatus.Enabled = false;
                btnUpdateMemberSave.Enabled = false;
                btnDeleteMember.Enabled = false;
                btnUpdateBookSave.Enabled = false;
                btnDeleteBook.Enabled = false;
                btnSaveUnP.Enabled = false;
            }
            else if (loginAs == "Employee")
            {
                btnSettingChange.Enabled = false;
                btnSettingDefault.Enabled = false;
            }
        }
        public void HideAll()           // To Hide all Panels added on the form.
        {
            pnlAddMember.Visible = false;
            pnlAddBook.Visible = false;
            pnlIssueBook.Visible = false;
            pnlViewMember.Visible = false;
            pnlViewBook.Visible = false;
            pnlViewIssued.Visible = false;
            pnlBookReturn.Visible = false;
            pnlUpdateBook.Visible = false;
            pnlUpdateMember.Visible = false;
            pnlDataSetting.Visible = false;
            pnlAccountSetting.Visible = false;
        }
        public void ClearIssue()        // To Clear Issue Panel.
        {
            cmbIssueMemberID.Text = "";
            cmbIssueBookID.Text = "";
            txtIssueMemberName.Text = "";
            txtIssueMemberRollNo.Text = "";
            txtIssueMemberBranch.Text = "";
            txtIssueBookName.Text = "";
            txtIssueBookCategory.Text = "";
            txtIssueBookAuthor.Text = "";
        }
        public void ClearReturn()       // To Clear Return Panel.
        {
            cmbReturnBookID.Text = "";
            txtReturnMemberID.Text = "";
            txtReturnMemberName.Text = "";
            txtReturnRollNo.Text = "";
            txtReturnBranch.Text = "";
            txtReturnBookName.Text = "";
            txtReturnCategory.Text = "";
            txtReturnAuthor.Text = "";
        }
        public void ClearAddMember()    // To Clear Add Member Panel.
        {
            txtAddMemberID.Text = "";
            txtAddMemberName.Text = "";
            txtAddRoll.Text = "";
            cmbAddBranch.Text = "";
            cmbAddSemester.Text = "";
            cmbAddGender.Text = "";
            txtAddPhone.Text = "";
            txtAddEmail.Text = "";
            txtAddAddress.Text = "";
        }
        public void ClearUpdateMember() // To Clear Update Member Panel.
        {
            txtUpdateMemberID.Text = "";
            txtUpdateMemberName.Text = "";
            txtUpdateRoll.Text = "";
            cmbUpdateBranch.Text = "";
            cmbUpdateSemester.Text = "";
            cmbUpdateGender.Text = "";
            txtUpdatePhone.Text = "";
            txtUpdateEmail.Text = "";
            txtUpdatetAddress.Text = "";
        }
        public void ClearAddBook()      // To Clear Add Book Panel.
        {
            txtAddBookID.Text = "";
            cmbAddBookName.Text = "";
            cmbAddBookCategory.Text = "";
            txtAddBookEdition.Text = "";
            cmbAddBookAuthor.Text = "";
            cmbAddBookPublisher.Text = "";
            txtAddBookPrice.Text = "";
        }
        public void ClearUpdateBook()   // To Clear Update Book Panel.
        {
            txtUpdateBookID.Text = "";
            cmbUpdateBookName.Text = "";
            cmbUpdateCategory.Text = "";
            txtUpdateEdition.Text = "";
            cmbUpdateAuthor.Text = "";
            cmbUpdatePublisher.Text = "";
            txtUpdatePrice.Text = "";
        }
        public void ClearAll()          // To Clear All Panel.
        {
            ClearIssue();
            ClearReturn();
            ClearAddMember();
            ClearUpdateMember();
            ClearAddBook();
            ClearUpdateBook();
        }
        public void LoadIssueData()     // To Load all Combo Box Data for Issue Panel.
        {
            string sql;
            try
            {
                cn.Open();

                sql = "Select Book_ID AS BookID From Book Where Issued_To_Member = '-'";
                da = new SqlDataAdapter(sql, cn);
                ds = new DataSet();
                da.Fill(ds);
                cmbIssueBookID.DataSource = ds.Tables[0];
                cmbIssueBookID.DisplayMember = "BookID";

                sql = "Select Member_ID AS MemberID From Member Where Issued_Book = '-'";
                da = new SqlDataAdapter(sql, cn);
                ds = new DataSet();
                da.Fill(ds);
                cmbIssueMemberID.DataSource = ds.Tables[0];
                cmbIssueMemberID.DisplayMember = "MemberID";

                cn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void LoadReturnData()    // To Load all Combo Box Data for Return Panel.
        {
            string sql;
            try
            {
                cn.Open();
                sql = "Select Book_ID AS BookID From Issue";
                da = new SqlDataAdapter(sql, cn);
                ds = new DataSet();
                da.Fill(ds);
                cmbReturnBookID.DataSource = ds.Tables[0];
                cmbReturnBookID.DisplayMember = "BookID";
                cn.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void LoadBookData()      // To Load all Combo Box Data for Add Book and Update Book Panels.
        {
            string sql;
            try
            {
                cn.Open();

                sql = "Select Name AS BookName From Book";
                da = new SqlDataAdapter(sql, cn);
                ds = new DataSet();
                da.Fill(ds);
                cmbAddBookName.DataSource = ds.Tables[0];
                cmbAddBookName.DisplayMember = "BookName";
                cmbUpdateBookName.DataSource = ds.Tables[0];
                cmbUpdateBookName.DisplayMember = "BookName";


                sql = "Select Category AS BookCat From Book";
                da = new SqlDataAdapter(sql, cn);
                ds = new DataSet();
                da.Fill(ds);
                cmbAddBookCategory.DataSource = ds.Tables[0];
                cmbAddBookCategory.DisplayMember = "BookCat";
                cmbUpdateCategory.DataSource = ds.Tables[0];
                cmbUpdateCategory.DisplayMember = "BookCat";

                sql = "Select Author AS BookAuthor From Book";
                da = new SqlDataAdapter(sql, cn);
                ds = new DataSet();
                da.Fill(ds);
                cmbAddBookAuthor.DataSource = ds.Tables[0];
                cmbAddBookAuthor.DisplayMember = "BookAuthor";
                cmbUpdateAuthor.DataSource = ds.Tables[0];
                cmbUpdateAuthor.DisplayMember = "BookAuthor";


                sql = "Select Publisher AS BookPublish From Book";
                da = new SqlDataAdapter(sql, cn);
                ds = new DataSet();
                da.Fill(ds);
                cmbAddBookPublisher.DataSource = ds.Tables[0];
                cmbAddBookPublisher.DisplayMember = "BookPublish";
                cmbUpdatePublisher.DataSource = ds.Tables[0];
                cmbUpdatePublisher.DisplayMember = "BookPublish";

                cn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void LoadAllData()       // To Load all Combo Box Data for All Panels.
        {
            LoadIssueData();
            LoadBookData();
            LoadReturnData();
        }
        public void LoadGridMember()    // To Load Members Grid View Data.
        {
            string str = "select * from Member ";
            try { 
                cn.Open();
                da = new SqlDataAdapter(str, cn);

                bld = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Member");
                dataGridViewMember.DataSource = ds.Tables["Member"];
                cn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            txtSearchMember.Focus();
        }
        public void LoadGridBook()      // To Load Books Grid View Data.
        {
            string str = "select * from Book";
            try
            {
                cn.Open();
                da = new SqlDataAdapter(str, cn);
                ds = new DataSet();
                da.Fill(ds, "Book");
                dataGridViewBook.DataSource = ds.Tables["Book"];
                cn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            txtSearchBook.Focus();
        }
        public void LoadGridIssue()      // To Load Books Grid View Data.
        {
            string str = "select * from Issue";
            try
            {
                cn.Open();
                da = new SqlDataAdapter(str, cn);
                ds = new DataSet();
                da.Fill(ds, "Issue");
                dataGridViewIssue.DataSource = ds.Tables["Issue"];
                cn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            txtSearchIssue.Focus();
        }
        public void ChangeSetting()     // To Control the functions on Setting Tab.
        {
            try
            {
                int days = Convert.ToInt32(txtSettingDays.Text);
                double rupee = Convert.ToDouble(txtSettingRupees.Text);

                cn.Open();
                cmd = new SqlCommand("Update Setting set Value = @rupee where Variable = 'Fine Charge'", cn);
                cmd.Parameters.AddWithValue("@rupee", rupee);
                cmd.ExecuteNonQuery();
                cn.Close();

                cn.Open();
                cmd = new SqlCommand("Update Setting set Value = @days where Variable = 'Maximum Days'", cn);
                cmd.Parameters.AddWithValue("@days", days);
                cmd.ExecuteNonQuery();
                cn.Close();
               
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid Value, Can't be change", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        public void LoadSetting()       // To Load Data for the functions on Setting Tab.
        {
            try
            {
                cn.Open();
                cmd = new SqlCommand("Select Value From Setting where Variable = 'Maximum Days'", cn);
                dr = cmd.ExecuteReader();
                dr.Read();
                txtSettingDays.Text = dr[0].ToString();
                maxDays = Convert.ToInt32(dr[0].ToString());
                cn.Close();

                cn.Open();
                cmd = new SqlCommand("Select Value From Setting where Variable = 'Fine Charge'", cn);
                dr = cmd.ExecuteReader();
                dr.Read();
                txtSettingRupees.Text = dr[0].ToString();
                fineCharge = Convert.ToDouble(dr[0].ToString());
                cn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public string ToStringIndianDateTime(DateTime date)
        {
            string sDate = date.ToString("dddd.  dd MMMM yyyy,  hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            return sDate;
        }
        public int DaysBetween(string sDate1, string sDate2)
        {
            CultureInfo MyCultureInfo = new CultureInfo("fr-FR");
            DateTime d1 = DateTime.Parse(sDate1, MyCultureInfo);
            DateTime d2 = DateTime.Parse(sDate2, MyCultureInfo);
            TimeSpan ts = d2.Subtract(d1);
            int result = Convert.ToInt32((ts.TotalDays));
            return result;
        }
        public void ShowFine(string memberID)
        {
            lblFine.Visible = true;
            try
            {
                cn.Open();
                cmd = new SqlCommand("Select Issue_Date From Issue where Member_ID = @memberID", cn);
                cmd.Parameters.AddWithValue("@memberID", memberID);
                dr = cmd.ExecuteReader();
                dr.Read();
                string sDate1 = dr[0].ToString();
                cn.Close();
                string sDate2 = ToStringIndianDateTime(dateTimePicker.Value);
                int totalDays = DaysBetween(sDate1, sDate2);
                if (totalDays > maxDays)
                {
                    double totalFine = fineCharge * (totalDays - maxDays);
                    lblFine.ForeColor = Color.Red;
                    lblFine.Text = "Fine is : " + totalFine + " Rupees";
                }
                else
                {
                    lblFine.ForeColor = Color.Green;
                    lblFine.Text = "No Fine";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnSettingChange_Click(object sender, EventArgs e)
        {
            btnSettingChange.Enabled = false;
            txtSettingRupees.Enabled = true;
            txtSettingDays.Enabled = true;
            btnSettingCancel.Enabled = true;
            btnSettingSave.Enabled = true;
        }
        private void btnSettingDefault_Click(object sender, EventArgs e)
        {
            txtSettingDays.Text = "15";
            txtSettingRupees.Text = "1";
            ChangeSetting();
            LoadSetting();
        }
        private void btnSettingCancel_Click(object sender, EventArgs e)
        {
            LoadSetting();
            txtSettingRupees.Enabled = false;
            txtSettingDays.Enabled = false;
            btnSettingCancel.Enabled = false;
            btnSettingSave.Enabled = false;
            btnSettingChange.Enabled = true;
        }
        private void btnSettingSave_Click(object sender, EventArgs e)
        {
            ChangeSetting();

            txtSettingRupees.Enabled = false;
            txtSettingDays.Enabled = false;
            btnSettingCancel.Enabled = false;
            btnSettingSave.Enabled = false;
            btnSettingChange.Enabled = true;
            LoadSetting();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            HideAll();
            pnlIssueBook.Visible = true;
            LoadIssueData();
            ClearIssue();
        }
        private void btnIssueCheck_Click(object sender, EventArgs e)
        {
            string memberID = cmbIssueMemberID.Text;
            string bookID = cmbIssueBookID.Text;
            string issuedToMember, issuedBook;
            btnIssueBook.Enabled = true;

            try
            {
                cn.Open();
                cmd = new SqlCommand("Select Name, Roll_No, Branch, Issued_Book From Member where Member_ID = @memberID", cn);

                cmd.Parameters.AddWithValue("memberID", memberID);
                dr = cmd.ExecuteReader();
                dr.Read();
                txtIssueMemberName.Text = dr[0].ToString();
                txtIssueMemberRollNo.Text = dr[1].ToString();
                txtIssueMemberBranch.Text = dr[2].ToString();
                issuedBook = dr[3].ToString();
                cn.Close();
                if (issuedBook != "-")
                {
                    MessageBox.Show("Member Already Have Book with Book ID : " + issuedBook, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnIssueBook.Enabled = false;
                }
            }
            catch (InvalidOperationException)
            {
                txtIssueMemberName.Text = "Not Found";
                txtIssueMemberRollNo.Text = "Not Found";
                txtIssueMemberBranch.Text = "Not Found";
                cn.Close();
                btnIssueBook.Enabled = false;
            }
            try
            {
                cn.Open();
                cmd = new SqlCommand("Select Name, Author, Category, Issued_To_Member From Book where Book_ID = @bookID", cn);

                cmd.Parameters.AddWithValue("@bookID", bookID);
                dr = cmd.ExecuteReader();
                dr.Read();
                txtIssueBookName.Text = dr[0].ToString();
                txtIssueBookAuthor.Text = dr[1].ToString();
                txtIssueBookCategory.Text = dr[2].ToString();
                issuedToMember = dr[3].ToString();
                cn.Close();
                if (issuedToMember != "-")
                {
                    MessageBox.Show("Book Already Issued to Member ID : " + issuedToMember, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnIssueBook.Enabled = false;
                }
            }
            catch (InvalidOperationException)
            {
                txtIssueBookName.Text = "Not Found";
                txtIssueBookAuthor.Text = "Not Found";
                txtIssueBookCategory.Text = "Not Found";
                cn.Close();
                btnIssueBook.Enabled = false;
            }
        }
        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            string bookID = cmbIssueBookID.Text;
            string memberID = cmbIssueMemberID.Text;
            string dateIssue = ToStringIndianDateTime(dateTimePicker.Value);
            cn.Open();
            string sql = "Insert Into Issue values (@Member_ID, @Book_ID, @Issue_Date)";
            cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@Member_ID", memberID);
            cmd.Parameters.AddWithValue("@Book_ID", bookID);
            cmd.Parameters.AddWithValue("@Issue_Date", dateIssue);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("Update Book set Issued_To_Member = @Member_ID where Book_ID = @Book_ID", cn);
            cmd.Parameters.AddWithValue("@Member_ID", memberID);
            cmd.Parameters.AddWithValue("@Book_ID", bookID);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("Update Member set Issued_Book = @Book_ID where Member_ID = @Member_ID", cn);
            cmd.Parameters.AddWithValue("@Book_ID", bookID);
            cmd.Parameters.AddWithValue("@Member_ID", memberID);
            cmd.ExecuteNonQuery();
            cn.Close();

            MessageBox.Show("Book Issued!!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnIssueBook.Enabled = false;
            LoadIssueData();
            ClearIssue();
        }
        private void btnIssueReset_Click(object sender, EventArgs e)
        {
            ClearIssue();
            btnIssueBook.Enabled = false;
        }
        
        private void btnReturn_Click(object sender, EventArgs e)
        {
            HideAll();
            pnlBookReturn.Visible = true;
            LoadReturnData();
            ClearReturn();
        }
        private void btnReturnStatus_Click(object sender, EventArgs e)
        {
            string bookID = cmbReturnBookID.Text;
            string memberID = "";
            btnReturnBook.Enabled = true;

            try
            {
                cn.Open();
                cmd = new SqlCommand("Select Name, Author, Category, Issued_To_Member From Book where Book_ID = @Book_ID", cn);
                cmd.Parameters.AddWithValue("@Book_ID", bookID);
                dr = cmd.ExecuteReader();
                dr.Read();
                txtReturnBookName.Text = dr[0].ToString();
                txtReturnAuthor.Text = dr[1].ToString();
                txtReturnCategory.Text = dr[2].ToString();
                memberID = dr[3].ToString();
                cn.Close();
            }
            catch (InvalidOperationException)
            {
                txtReturnBookName.Text = "Not Found";
                txtReturnAuthor.Text = "Not Found";
                txtReturnCategory.Text = "Not Found";
                cn.Close();
                btnReturnBook.Enabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            try
            {
                cn.Open();
                cmd = new SqlCommand("Select Name, Roll_No, Branch From Member where Member_ID = @memberID", cn);
                cmd.Parameters.AddWithValue("@memberID", memberID); ;
                dr = cmd.ExecuteReader();
                dr.Read();
                txtReturnMemberID.Text = memberID;
                txtReturnMemberName.Text = dr[0].ToString();
                txtReturnRollNo.Text = dr[1].ToString();
                txtReturnBranch.Text = dr[2].ToString();
                cn.Close();
                ShowFine(memberID);
            }
            catch (InvalidOperationException)
            {
                txtReturnMemberName.Text = "Not Found";
                txtReturnRollNo.Text = "Not Found";
                txtReturnBranch.Text = "Not Found";
                cn.Close();
                btnReturnBook.Enabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            string bookID = cmbReturnBookID.Text;
            string memberID = txtReturnMemberID.Text;
            if (cmbReturnBookID.Text == "")
            {
                MessageBox.Show("**You must Enter the Book ID!!");
            }
            else
            {
                try
                {
                    cn.Open();
                    cmd = new SqlCommand("Delete From Issue where Book_ID = @bookID", cn);
                    cmd.Parameters.AddWithValue("@bookID", bookID);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Update Member set Issued_Book = '-' where Member_ID = @Member_ID", cn);
                    cmd.Parameters.AddWithValue("@Member_ID", memberID);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Update Book set Issued_To_Member = '-' where Book_ID = @Book_ID", cn);
                    cmd.Parameters.AddWithValue("@Book_ID", bookID);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Returned!!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnReturnBook.Enabled = false;
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                ClearReturn();
                LoadReturnData();
            }
        }
        private void btnReturnReset_Click(object sender, EventArgs e)
        {
            ClearReturn();
            btnReturnBook.Enabled = false;
        }
        private void btnIssuedDetail_Click(object sender, EventArgs e)
        {
            if (dataGridViewIssue.CurrentRow.Index == -1)
            {
                MessageBox.Show("****You must select a record View Detail!!");
            }
            else
            {
                detailMemberID = dataGridViewIssue.CurrentRow.Cells[0].Value.ToString();
                AboutBoxMember DetailMember = new AboutBoxMember();
                DetailMember.Show();
            }
        }
       
        private void btnAddMember_Click_1(object sender, EventArgs e)
        {
            HideAll();
            pnlAddMember.Visible = true;
            ClearAddMember();
        }
        private void btnSaveMember_Click(object sender, EventArgs e)
        {

            string name, roll, ID, add, sem, branch, gen, phone, email, issueBook;

            name = txtAddMemberName.Text;
            roll = txtAddRoll.Text;
            ID = txtAddMemberID.Text;
            add = txtAddAddress.Text;
            sem = cmbAddSemester.Text;
            branch = cmbAddBranch.Text;
            gen = cmbAddGender.Text;
            phone = txtAddPhone.Text;
            email = txtAddEmail.Text;
            issueBook = "-";
            errorProvider.Clear();
            if (txtAddMemberID.Text == "")
            {
                errorProvider.SetError(txtAddMemberID, "**Please Enter Member ID!!");
                txtAddMemberID.Focus();
            }
            else if (txtAddPhone.Text == "")
            {
                errorProvider.SetError(txtAddPhone, "**Phone Number Must Requred!!");
            }
            else
            {
                try
                {
                    cn.Open();
                    string sql = "Insert Into [Library].[dbo].[Member] values (@Member_ID, @Name, @Roll_No, @Branch, @Semester, @Gender, @Phone, @Email, @Address, @Issued_Book)";
                    cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@Member_ID", ID);
                    cmd.Parameters.AddWithValue("@Roll_No", roll);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Branch", branch);
                    cmd.Parameters.AddWithValue("@Semester", sem);
                    cmd.Parameters.AddWithValue("@Gender", gen);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Address", add);
                    cmd.Parameters.AddWithValue("@Issued_Book", issueBook);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Member Record Saved!");
                    cn.Close();
                    ClearAddMember();
                }
                catch (SqlException excp)
                {
                    string exc = excp.ToString();
                    MessageBox.Show("Member ID Already Exists!!"+exc);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        private void btnAddMemberReset_Click(object sender, EventArgs e)
        {
            ClearAddMember();
        }
        private void btnViewMember_Click(object sender, EventArgs e)
        {
            HideAll();
            pnlViewMember.Visible = true;
            LoadGridMember();
            txtSearchMember.Focus();

        }
        private void rbtnSearchByMemberID_CheckedChanged(object sender, EventArgs e)
        {
            lblSearchMember.Text = "Search By Member ID :";
            txtSearchMember.Focus();
        }
        private void rbtnSearchByName_CheckedChanged(object sender, EventArgs e)
        {
            lblSearchMember.Text = "Search By Name :";
            txtSearchMember.Focus(); ;
        }
        private void rbtnSearchByRollNo_CheckedChanged(object sender, EventArgs e)
        {
            lblSearchMember.Text = "Search By Roll No. :";
            txtSearchMember.Focus();
        }
        private void rbtnSearchByBranch_CheckedChanged(object sender, EventArgs e)
        {
            lblSearchMember.Text = "Search By Branch :";
            txtSearchMember.Focus();
        }
        private void rbtnSearchBySemester_CheckedChanged(object sender, EventArgs e)
        {
            lblSearchMember.Text = "Search By Semester :";
            txtSearchMember.Focus();
        }
        private void txtSearchMember_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearchMember.Text + "%";

            if (rbtnSearchByMemberID.Checked == true)
            {
                string str = "Select * from Member where Member_ID like '" + search + "'";
                cn.Open();
                da = new SqlDataAdapter(str, cn);

                bld = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Member");
                dataGridViewMember.DataSource = ds.Tables["Member"];
                cn.Close();
            }
            else if (rbtnSearchByName.Checked == true)
            {
                string str = "select * from Member where Name like '" + search + "'";
                cn.Open();
                da = new SqlDataAdapter(str, cn);

                bld = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Member");
                dataGridViewMember.DataSource = ds.Tables["Member"];
                cn.Close();
            }
            else if (rbtnSearchByRollNo.Checked == true)
            {
                string str = "Select * From Member where Roll_No like '" + search + "'";
                try
                {
                    cn.Open();
                    da = new SqlDataAdapter(str, cn);

                    bld = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "Member");
                    dataGridViewMember.DataSource = ds.Tables["Member"];
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else if (rbtnSearchByBranch.Checked == true)
            {
                string str = "Select * from Member where Branch like '" + search + "'";
                cn.Open();
                da = new SqlDataAdapter(str, cn);

                bld = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Member");
                dataGridViewMember.DataSource = ds.Tables["Member"];
                cn.Close();
            }

            else if (rbtnSearchBySemester.Checked == true)
            {
                string str = "Select * from Member where Semester like '" + search + "'";
                cn.Open();
                da = new SqlDataAdapter(str, cn);

                bld = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Member");
                dataGridViewMember.DataSource = ds.Tables["Member"];
                cn.Close();
            }
        }
        private void btnUpdateMember_Click(object sender, EventArgs e)
        {
            HideAll();
            pnlViewMember.Visible = true;
            pnlUpdateMember.Visible = true;


            if (dataGridViewMember.CurrentRow.Index == -1)
            {
                MessageBox.Show("****You must select a record to Update!!");
            }
            else
            {
                string memberID = dataGridViewMember.CurrentRow.Cells[0].Value.ToString();

                cn.Open();
                cmd = new SqlCommand("Select * From Member where Member_ID = @memberID", cn);
                cmd.Parameters.AddWithValue("@memberID", memberID);
                dr = cmd.ExecuteReader();
                dr.Read();

                txtUpdateMemberID.Text = dr[0].ToString();
                txtUpdateMemberName.Text = dr[1].ToString();
                txtUpdateRoll.Text = dr[2].ToString();
                cmbUpdateBranch.Text = dr[3].ToString();
                cmbUpdateSemester.Text = dr[4].ToString();
                cmbUpdateGender.Text = dr[5].ToString();
                txtUpdatePhone.Text = dr[6].ToString();
                txtUpdateEmail.Text = dr[7].ToString();
                txtUpdatetAddress.Text = dr[8].ToString();
                cmbUpdateBookName.Focus();
                cn.Close();
            }
        }
        private void btnUpdateMemberSave_Click(object sender, EventArgs e)
        {

            string memberID, name, roll, branch, sem, gen, phone, email, add;

            memberID = txtUpdateMemberID.Text;
            name = txtUpdateMemberName.Text;
            roll = txtUpdateRoll.Text;
            branch = cmbUpdateBranch.Text;
            sem = cmbUpdateSemester.Text;
            gen = cmbUpdateGender.Text;
            phone = txtUpdatePhone.Text;
            email = txtUpdateEmail.Text;
            add = txtUpdatetAddress.Text;

            cn.Open();
            cmd = new SqlCommand("Update Member set Name = @Name, Roll_No = @Roll_No, Branch = @Branch, Semester = @Semester, Gender = @Gender, Phone = @Phone, Email = @Email, Address = @Address where Member_ID = @Member_ID", cn);
            cmd.Parameters.AddWithValue("@Member_ID", memberID);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Roll_No", roll);
            cmd.Parameters.AddWithValue("@Branch", branch);
            cmd.Parameters.AddWithValue("@Semester", sem);
            cmd.Parameters.AddWithValue("@Gender", gen);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Address", add);
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Update Successful!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadGridMember();
            ClearUpdateMember();
        }
        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            string memberID = dataGridViewMember.CurrentRow.Cells[0].Value.ToString();
            
            cn.Open();
            cmd = new SqlCommand("Select Issued_Book From Member where Member_ID = @memberID", cn);
            cmd.Parameters.AddWithValue("@memberID", memberID);
            dr = cmd.ExecuteReader();
            dr.Read();
            string issuedBook = dr[0].ToString();
            cn.Close();

            if (issuedBook == "-")
            {
                cn.Open();
                cmd = new SqlCommand("Delete From Member where Member_ID = @memberID And Issued_Book = '-'", cn);
                cmd.Parameters.AddWithValue("@memberID", memberID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Selected Member Record Deleted!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.Close();
            }
            else
            {
                MessageBox.Show("A book have issued to this member, \nCan't be delete!!", "Can't process!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadGridMember();
            txtSearchMember.Focus();
        }
        private void btnDetailMember_Click(object sender, EventArgs e)
        {
            if (dataGridViewMember.CurrentRow.Index == -1)
            {
                MessageBox.Show("****You must select a record View Detail!!");
            }
            else
            {
                detailMemberID = dataGridViewMember.CurrentRow.Cells[0].Value.ToString();
                AboutBoxMember DetailMember = new AboutBoxMember();
                DetailMember.Show();
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            HideAll();
            pnlAddBook.Visible = true;
            LoadBookData();
            ClearAddBook();
        }
        private void btnSaveBook_Click(object sender, EventArgs e)
        {
            try
            {
                string name, ID, author, category, publisher, edition, issuedToMember;
                name = cmbAddBookName.Text;
                ID = txtAddBookID.Text;
                category = cmbAddBookCategory.Text;
                author = cmbAddBookAuthor.Text;
                publisher = cmbAddBookPublisher.Text;
                edition = txtAddBookEdition.Text;
                double price = Convert.ToDouble(txtAddBookPrice.Text);
                issuedToMember = "-";
                if (ID == "")
                {
                    errorProvider.SetError(txtAddBookID,"**Book ID must Required!!");
                }
                else
                {
                    cn.Open();
                    string sql = "Insert Into Book values (@Book_ID, @Name, @Category, @Author, @Publisher, @Edition, @Price, @Issued_To_Member)";
                    cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@Book_ID", ID);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@Author", author);
                    cmd.Parameters.AddWithValue("@Publisher", publisher);
                    cmd.Parameters.AddWithValue("@Edition", edition);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Issued_To_Member", issuedToMember);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Record Saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();
                    ClearAddBook();
                    txtAddBookID.Focus();
                }
            }
            catch (FormatException)
            {
                errorProvider.SetError(txtAddBookPrice, "Invalid Price!!");
                cn.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Book ID Already Exists!!");
                cn.Close();
            }
        }
        private void btnAddBookReset_Click(object sender, EventArgs e)
        {
            ClearAddBook();
        }
        private void btnUpdateMemberReset_Click(object sender, EventArgs e)
        {
            ClearUpdateMember();
        }
        private void btnViewBook_Click(object sender, EventArgs e)
        {
            HideAll();
            pnlViewBook.Visible = true;

            LoadGridBook();
            txtSearchBook.Focus();
        }
        private void rbtnByBookID_CheckedChanged(object sender, EventArgs e)
        {
            lblSreachBook.Text = "Search By Book ID :";
            txtSearchBook.Focus();
        }
        private void rbtnByBookName_CheckedChanged(object sender, EventArgs e)
        {
            lblSreachBook.Text = "Search By Book Name :";
            txtSearchBook.Focus();
        }
        private void rbtnByAuthorName_CheckedChanged(object sender, EventArgs e)
        {
            lblSreachBook.Text = "Search By Author Name :";
            txtSearchBook.Focus();
        }
        private void rbtnByBookCategory_CheckedChanged(object sender, EventArgs e)
        {
            lblSreachBook.Text = "Search By Book Category :";
            txtSearchBook.Focus();
        }
        private void txtSearchBook_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearchBook.Text + "%";

            if (rbtnByBookID.Checked == true)
            {
                string str = "Select * from Book where Book_ID like '" + search + "'";
                cn.Open();
                da = new SqlDataAdapter(str, cn);

                bld = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Book");
                dataGridViewBook.DataSource = ds.Tables["Book"];
                cn.Close();
            }
            else if (rbtnByBookName.Checked == true)
            {
                string str = "select * from Book where Name like '" + search + "'";
                cn.Open();
                da = new SqlDataAdapter(str, cn);

                bld = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Book");
                dataGridViewBook.DataSource = ds.Tables["Book"];
                cn.Close();
            }
            else if (rbtnByAuthorName.Checked == true)
            {
                string str = "Select * From Book where Author like '" + search + "'";
                cn.Open();
                da = new SqlDataAdapter(str, cn);

                bld = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Book");
                dataGridViewBook.DataSource = ds.Tables["Book"];
                cn.Close();
            }
            else if (rbtnByBookCategory.Checked == true)
            {
                string str = "Select * From Book where Category like '" + search + "'";
                cn.Open();
                da = new SqlDataAdapter(str, cn);

                bld = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Book");
                dataGridViewBook.DataSource = ds.Tables["Book"];
                cn.Close();
            }
        }
        private void btnUpdateBook_Click(object sender, EventArgs e)
        {
            HideAll();
            pnlViewBook.Visible = true;
            pnlUpdateBook.Visible = true;

            if (dataGridViewBook.CurrentRow.Index == -1)
            {
                MessageBox.Show("****You must select a record to Update!!");
            }
            else
            {
                string bookID = dataGridViewBook.CurrentRow.Cells[0].Value.ToString();

                cn.Open();
                cmd = new SqlCommand("Select * From Book where Book_ID = @bookID", cn);
                cmd.Parameters.AddWithValue("bookID", bookID);
                dr = cmd.ExecuteReader();
                dr.Read();

                txtUpdateBookID.Text = dr[0].ToString();
                cmbUpdateBookName.Text = dr[1].ToString();
                cmbUpdateCategory.Text = dr[2].ToString();
                txtUpdateEdition.Text = dr[3].ToString();
                cmbUpdateAuthor.Text = dr[4].ToString();
                cmbUpdatePublisher.Text = dr[5].ToString();
                txtUpdatePrice.Text = dr[6].ToString();
                cmbUpdateBookName.Focus();
                cn.Close();
            }
        }
        private void btnUpdateBookSave_Click(object sender, EventArgs e)
        {
            string bookID, name, author, category, publisher, price, edition;
            bookID = txtUpdateBookID.Text;
            name = cmbUpdateBookName.Text;
            category = cmbUpdateCategory.Text;
            author = cmbUpdateAuthor.Text;
            publisher = cmbUpdatePublisher.Text;
            edition = txtUpdateEdition.Text;
            price = txtUpdatePrice.Text;
            cn.Open();
            cmd = new SqlCommand("Update Book set Name = @Name, Author = @Author, Category = @Category, Publisher = @Publisher, Price = @Price, Edition = @Edition where Book_ID = @Book_ID", cn);
            cmd.Parameters.AddWithValue("@Book_ID", bookID);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Author", author);
            cmd.Parameters.AddWithValue("@Category", category);
            cmd.Parameters.AddWithValue("@Publisher", publisher);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Edition", edition);
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Update Successful!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearUpdateBook();
            LoadGridBook();
        }
        private void btnUpdateBookReset_Click(object sender, EventArgs e)
        {
            ClearUpdateBook();
        }
        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            string bookID = dataGridViewBook.CurrentRow.Cells[0].Value.ToString();

            cn.Open();
            cmd = new SqlCommand("Select Issued_To_Member From Book where Book_ID = @bookID", cn);
            cmd.Parameters.AddWithValue("@bookID", bookID);
            dr = cmd.ExecuteReader();
            dr.Read();
            string issuedToMember = dr[0].ToString();
            cn.Close();
            
            if(issuedToMember=="-")
            {
                cn.Open();
                cmd = new SqlCommand("Delete From Book where Book_ID = @bookID", cn);
                cmd.Parameters.AddWithValue("@bookID", bookID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seclected Record Book Deleted!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.Close();
            }
            else
            {
                MessageBox.Show("This Book has issued to a member, \nCan't be delete!!", "Can't process!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadGridBook();
            txtSearchBook.Focus();
        }
        private void btnDetailBook_Click(object sender, EventArgs e)
        {
            if (dataGridViewBook.CurrentRow.Index == -1)
            {
                MessageBox.Show("****You must select a record View Detail!!");
            }
            else
            {
                detailBookID = dataGridViewBook.CurrentRow.Cells[0].Value.ToString();
                AboutBoxBook DetailBook = new AboutBoxBook();
                DetailBook.Show();
            }
        }

        private void txtAddPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void btnDataSetting_Click(object sender, EventArgs e)
        {
            HideAll();
            pnlDataSetting.Visible = true;
        }
        private void rBtnByMemberIDIssue_CheckedChanged(object sender, EventArgs e)
        {
            lblSearchIssue.Text = "Search by Member ID :";
        }
        private void rBtnByBookIDIssue_CheckedChanged(object sender, EventArgs e)
        {
            lblSearchIssue.Text = "Search by Book ID :";
        }
        private void rBtnByMemberNameIssue_CheckedChanged(object sender, EventArgs e)
        {
            lblSearchIssue.Text = "Search by Member Name :";
        }
        private void rBtnByBookNameIssue_CheckedChanged(object sender, EventArgs e)
        {
            lblSearchIssue.Text = "Search by Book Name :";
        }
        private void txtSearchIssue_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearchIssue.Text + "%";

            if (rBtnByMemberIDIssue.Checked == true)
            {
                string str = "Select * from Issue where Member_ID like '" + search + "'";
                cn.Open();
                da = new SqlDataAdapter(str, cn);

                bld = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Issue");
                dataGridViewIssue.DataSource = ds.Tables["Issue"];
                cn.Close();
            }
            else if (rBtnByBookIDIssue.Checked == true)
            {
                string str = "Select * from Issue where Book_ID like '" + search + "'";
                cn.Open();
                da = new SqlDataAdapter(str, cn);

                bld = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Issue");
                dataGridViewIssue.DataSource = ds.Tables["Issue"];
                cn.Close();
            }
        }
        private void btnViewIssued_Click(object sender, EventArgs e)
        {
            HideAll();
            pnlViewIssued.Visible = true;
            LoadGridIssue();
        }
        
        private void btnAcountSetting_Click(object sender, EventArgs e)
        {
            HideAll();
            pnlAccountSetting.Visible = true;
            lblLoggedInAs.Text = loginAs;
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            this.Close();
            login.Show();
        }
        private void btnChangeUnP_Click(object sender, EventArgs e)
        {
            grpBoxChange.Visible = true;
            if (loginAs == "Administrator")
            {
                txtUsername.Text = "Administrator";
                txtUsername.Enabled = false;
                txtNewUsername.Enabled = false;
                txtPassword.Focus();
            }
            else
            {
                txtUsername.Enabled = true;
                txtNewUsername.Enabled = true;
                txtUsername.Focus();
            }
        }
        private void btnCancelChange_Click(object sender, EventArgs e)
        {
            lblInvalidMessage.Visible = false;
            grpBoxChange.Visible = false;
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveUnP_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            
            if (txtUsername.Text=="")
            {
                errorProvider.SetError(txtUsername, "**Please Enter Current Username");
                lblInvalidMessage.Text="**Please Enter Current Username";
            }
            else if(txtPassword.Text=="")
            {
                errorProvider.SetError(txtPassword, "**Please Enter Password");
                lblInvalidMessage.Text = "**Please Enter Password";
            }
            else if (txtNewUsername.Text=="")
            {
                errorProvider.SetError(txtNewUsername, "**Please Enter New Username");
                lblInvalidMessage.Text="**Please Enter New Username";
            }
            else if (txtNewPassword.Text=="")
            {
                errorProvider.SetError(txtNewPassword, "**Please Enter New Password");
                lblInvalidMessage.Text="**Please Enter New Password";
            }
            else if (txtConfirmPassword.Text=="")
            {
                errorProvider.SetError(txtConfirmPassword, "**Please Enter Confirm Password");
                lblInvalidMessage.Text="**Please Enter Confirm Password";
            }
            else
            {
                string adminPassword, empUsername, empPassword;
                string currentUsername=txtUsername.Text, currentPassword=txtPassword.Text;
                string newUsername=txtNewUsername.Text, newPassword=txtNewPassword.Text, confirmPassword=txtConfirmPassword.Text;
                
                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("New Password and Current Password are diffrent \nPlease Check!!", "Match Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if(loginAs == "Administrator")
                {
                    cn.Open();
                    cmd = new SqlCommand("Select Value From Setting where Variable = 'Admin Password'", cn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    adminPassword = dr[0].ToString();
                    cn.Close();

                    if (adminPassword==currentPassword)
                    {
                        cn.Open();
                        cmd = new SqlCommand("Update Setting set Value = @newPassword where Variable = 'Admin Password'", cn);
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);
                        cmd.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Administrator Password Changed!!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        lblInvalidMessage.Visible = true;
                        lblInvalidMessage.Text = "*~Invalid Administrator's Password~*";
                    }
                }
                else if (loginAs=="Employee")
                {
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

                    if (empUsername==currentUsername && empPassword==currentPassword)
                    {
                        cn.Open();
                        cmd = new SqlCommand("Update Setting set Value = @newUsername where Variable = 'Emp Username'", cn);
                        cmd.Parameters.AddWithValue("@newUsername", newUsername);
                        cmd.ExecuteNonQuery();
                        cn.Close();

                        cn.Open();
                        cmd = new SqlCommand("Update Setting set Value = @newPassword where Variable = 'Emp Password'", cn);
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);
                        cmd.ExecuteNonQuery();
                        cn.Close();
            
                        MessageBox.Show("Employee Username and Password Changed!!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        lblInvalidMessage.Visible = true;
                        lblInvalidMessage.Text = "*~Invalid Employee's Username Or Password~*";
                    }
                }
            }
        }
    }
 } 
 