namespace Library_Management
{
    partial class frmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblInvalidMessage = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.cmbLoginAs = new System.Windows.Forms.ComboBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblLoginAs = new System.Windows.Forms.Label();
            this.btnLoginClose = new System.Windows.Forms.Button();
            this.btnStartWithoutLogin = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lblInvalidMessage
            // 
            this.lblInvalidMessage.AutoSize = true;
            this.lblInvalidMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblInvalidMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvalidMessage.ForeColor = System.Drawing.Color.Red;
            this.lblInvalidMessage.Location = new System.Drawing.Point(257, 292);
            this.lblInvalidMessage.Name = "lblInvalidMessage";
            this.lblInvalidMessage.Size = new System.Drawing.Size(14, 17);
            this.lblInvalidMessage.TabIndex = 22;
            this.lblInvalidMessage.Text = "-";
            this.lblInvalidMessage.Visible = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPassword.ForeColor = System.Drawing.Color.Navy;
            this.txtPassword.Location = new System.Drawing.Point(253, 213);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(334, 30);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtUsername.ForeColor = System.Drawing.Color.Navy;
            this.txtUsername.Location = new System.Drawing.Point(253, 177);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(334, 30);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsername_KeyDown);
            // 
            // cmbLoginAs
            // 
            this.cmbLoginAs.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.cmbLoginAs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbLoginAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoginAs.ForeColor = System.Drawing.Color.Navy;
            this.cmbLoginAs.FormattingEnabled = true;
            this.cmbLoginAs.Items.AddRange(new object[] {
            "Administrator",
            "Employee"});
            this.cmbLoginAs.Location = new System.Drawing.Point(253, 138);
            this.cmbLoginAs.Name = "cmbLoginAs";
            this.cmbLoginAs.Size = new System.Drawing.Size(334, 33);
            this.cmbLoginAs.TabIndex = 0;
            this.cmbLoginAs.SelectedIndexChanged += new System.EventHandler(this.cmbLoginAs_SelectedIndexChanged);
            this.cmbLoginAs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLoginAs_KeyDown);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.Red;
            this.lblPassword.Location = new System.Drawing.Point(131, 217);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(119, 25);
            this.lblPassword.TabIndex = 15;
            this.lblPassword.Text = "Password :";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblUsername.Location = new System.Drawing.Point(131, 180);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(123, 25);
            this.lblUsername.TabIndex = 14;
            this.lblUsername.Text = "Username :";
            // 
            // lblLoginAs
            // 
            this.lblLoginAs.AutoSize = true;
            this.lblLoginAs.BackColor = System.Drawing.Color.Transparent;
            this.lblLoginAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginAs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblLoginAs.Location = new System.Drawing.Point(131, 143);
            this.lblLoginAs.Name = "lblLoginAs";
            this.lblLoginAs.Size = new System.Drawing.Size(107, 25);
            this.lblLoginAs.TabIndex = 13;
            this.lblLoginAs.Text = "Login as :";
            // 
            // btnLoginClose
            // 
            this.btnLoginClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoginClose.BackColor = System.Drawing.Color.Transparent;
            this.btnLoginClose.BackgroundImage = global::Library_Management.Properties.Resources.exit;
            this.btnLoginClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoginClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoginClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoginClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnLoginClose.Location = new System.Drawing.Point(574, 325);
            this.btnLoginClose.Name = "btnLoginClose";
            this.btnLoginClose.Size = new System.Drawing.Size(48, 36);
            this.btnLoginClose.TabIndex = 5;
            this.btnLoginClose.UseCompatibleTextRendering = true;
            this.btnLoginClose.UseVisualStyleBackColor = false;
            this.btnLoginClose.Click += new System.EventHandler(this.btnLoginClose_Click);
            // 
            // btnStartWithoutLogin
            // 
            this.btnStartWithoutLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartWithoutLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnStartWithoutLogin.Image = global::Library_Management.Properties.Resources.color_wheel_icon_cpy;
            this.btnStartWithoutLogin.Location = new System.Drawing.Point(253, 325);
            this.btnStartWithoutLogin.Name = "btnStartWithoutLogin";
            this.btnStartWithoutLogin.Size = new System.Drawing.Size(302, 36);
            this.btnStartWithoutLogin.TabIndex = 4;
            this.btnStartWithoutLogin.Text = "Start Without Login";
            this.btnStartWithoutLogin.UseCompatibleTextRendering = true;
            this.btnStartWithoutLogin.UseVisualStyleBackColor = true;
            this.btnStartWithoutLogin.Click += new System.EventHandler(this.btnStartWithoutLogin_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.White;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnLogin.Image = global::Library_Management.Properties.Resources.color_wheel_icon_cpy;
            this.btnLogin.Location = new System.Drawing.Point(253, 251);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(199, 36);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseCompatibleTextRendering = true;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(304, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 31);
            this.label1.TabIndex = 23;
            this.label1.Text = "Loging Page";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Library_Management.Properties.Resources.windows_7_wallpaper_7;
            this.ClientSize = new System.Drawing.Size(755, 436);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblInvalidMessage);
            this.Controls.Add(this.btnLoginClose);
            this.Controls.Add(this.btnStartWithoutLogin);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.cmbLoginAs);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblLoginAs);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmLogin";
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lblInvalidMessage;
        private System.Windows.Forms.Button btnLoginClose;
        private System.Windows.Forms.Button btnStartWithoutLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.ComboBox cmbLoginAs;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblLoginAs;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label1;
    }
}