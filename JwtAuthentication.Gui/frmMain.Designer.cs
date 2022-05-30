namespace JwtAuthentication.Gui
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabUserRegistration = new System.Windows.Forms.TabPage();
            this.chklRegistrationMediators = new System.Windows.Forms.CheckedListBox();
            this.btnRegistrationClear = new System.Windows.Forms.Button();
            this.txtRegistrationPassword = new System.Windows.Forms.TextBox();
            this.lblRegistrationPassword = new System.Windows.Forms.Label();
            this.txtRegistrationUsername = new System.Windows.Forms.TextBox();
            this.btnRegistrationSend = new System.Windows.Forms.Button();
            this.lblRegistrationUsername = new System.Windows.Forms.Label();
            this.tabUserAuthentication = new System.Windows.Forms.TabPage();
            this.chklAuthenticationMediators = new System.Windows.Forms.CheckedListBox();
            this.txtAuthenticationUsername = new System.Windows.Forms.ComboBox();
            this.btnAuthenticationClear = new System.Windows.Forms.Button();
            this.txtAuthenticationPassword = new System.Windows.Forms.TextBox();
            this.lblAuthenticationPassword = new System.Windows.Forms.Label();
            this.btnAuthenticationSend = new System.Windows.Forms.Button();
            this.lblUserAuthentication = new System.Windows.Forms.Label();
            this.tabMediatorRegistration = new System.Windows.Forms.TabPage();
            this.cboAlgoritm = new System.Windows.Forms.ComboBox();
            this.lblAlgorithm = new System.Windows.Forms.Label();
            this.nudExpiration = new System.Windows.Forms.NumericUpDown();
            this.btnMediatorClear = new System.Windows.Forms.Button();
            this.lblExpiration = new System.Windows.Forms.Label();
            this.txtMediatorName = new System.Windows.Forms.TextBox();
            this.btnMediator = new System.Windows.Forms.Button();
            this.lblMediatorName = new System.Windows.Forms.Label();
            this.tabMediatorAuthentication = new System.Windows.Forms.TabPage();
            this.cboMediatorAuthenticationUsername = new System.Windows.Forms.ComboBox();
            this.lblMediatorAuthenticationUsername = new System.Windows.Forms.Label();
            this.cboMediatorAuthenticationMediatorName = new System.Windows.Forms.ComboBox();
            this.lblMediatorAuthenticationMediatorName = new System.Windows.Forms.Label();
            this.btnMediatorAuthentication = new System.Windows.Forms.Button();
            this.lblMediatorAuthenticationClear = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabUserRegistration.SuspendLayout();
            this.tabUserAuthentication.SuspendLayout();
            this.tabMediatorRegistration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExpiration)).BeginInit();
            this.tabMediatorAuthentication.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabUserRegistration);
            this.tabMain.Controls.Add(this.tabUserAuthentication);
            this.tabMain.Controls.Add(this.tabMediatorRegistration);
            this.tabMain.Controls.Add(this.tabMediatorAuthentication);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(501, 146);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabUserRegistration
            // 
            this.tabUserRegistration.Controls.Add(this.chklRegistrationMediators);
            this.tabUserRegistration.Controls.Add(this.btnRegistrationClear);
            this.tabUserRegistration.Controls.Add(this.txtRegistrationPassword);
            this.tabUserRegistration.Controls.Add(this.lblRegistrationPassword);
            this.tabUserRegistration.Controls.Add(this.txtRegistrationUsername);
            this.tabUserRegistration.Controls.Add(this.btnRegistrationSend);
            this.tabUserRegistration.Controls.Add(this.lblRegistrationUsername);
            this.tabUserRegistration.Location = new System.Drawing.Point(4, 24);
            this.tabUserRegistration.Name = "tabUserRegistration";
            this.tabUserRegistration.Padding = new System.Windows.Forms.Padding(3);
            this.tabUserRegistration.Size = new System.Drawing.Size(493, 118);
            this.tabUserRegistration.TabIndex = 0;
            this.tabUserRegistration.Text = "User Registration";
            this.tabUserRegistration.UseVisualStyleBackColor = true;
            // 
            // chklRegistrationMediators
            // 
            this.chklRegistrationMediators.FormattingEnabled = true;
            this.chklRegistrationMediators.Location = new System.Drawing.Point(220, 16);
            this.chklRegistrationMediators.Name = "chklRegistrationMediators";
            this.chklRegistrationMediators.Size = new System.Drawing.Size(120, 76);
            this.chklRegistrationMediators.TabIndex = 11;
            // 
            // btnRegistrationClear
            // 
            this.btnRegistrationClear.Location = new System.Drawing.Point(401, 73);
            this.btnRegistrationClear.Name = "btnRegistrationClear";
            this.btnRegistrationClear.Size = new System.Drawing.Size(84, 23);
            this.btnRegistrationClear.TabIndex = 10;
            this.btnRegistrationClear.Text = "Clear";
            this.btnRegistrationClear.UseVisualStyleBackColor = true;
            this.btnRegistrationClear.Click += new System.EventHandler(this.BtnRegistrationClear_Click);
            // 
            // txtRegistrationPassword
            // 
            this.txtRegistrationPassword.Location = new System.Drawing.Point(84, 44);
            this.txtRegistrationPassword.Name = "txtRegistrationPassword";
            this.txtRegistrationPassword.Size = new System.Drawing.Size(100, 23);
            this.txtRegistrationPassword.TabIndex = 9;
            // 
            // lblRegistrationPassword
            // 
            this.lblRegistrationPassword.AutoSize = true;
            this.lblRegistrationPassword.Location = new System.Drawing.Point(18, 47);
            this.lblRegistrationPassword.Name = "lblRegistrationPassword";
            this.lblRegistrationPassword.Size = new System.Drawing.Size(57, 15);
            this.lblRegistrationPassword.TabIndex = 8;
            this.lblRegistrationPassword.Text = "Password";
            // 
            // txtRegistrationUsername
            // 
            this.txtRegistrationUsername.Location = new System.Drawing.Point(84, 16);
            this.txtRegistrationUsername.Name = "txtRegistrationUsername";
            this.txtRegistrationUsername.Size = new System.Drawing.Size(100, 23);
            this.txtRegistrationUsername.TabIndex = 7;
            // 
            // btnRegistrationSend
            // 
            this.btnRegistrationSend.Location = new System.Drawing.Point(18, 73);
            this.btnRegistrationSend.Name = "btnRegistrationSend";
            this.btnRegistrationSend.Size = new System.Drawing.Size(166, 23);
            this.btnRegistrationSend.TabIndex = 6;
            this.btnRegistrationSend.Text = "Send";
            this.btnRegistrationSend.UseVisualStyleBackColor = true;
            this.btnRegistrationSend.Click += new System.EventHandler(this.BtnRegistrationSend_Click);
            // 
            // lblRegistrationUsername
            // 
            this.lblRegistrationUsername.AutoSize = true;
            this.lblRegistrationUsername.Location = new System.Drawing.Point(18, 19);
            this.lblRegistrationUsername.Name = "lblRegistrationUsername";
            this.lblRegistrationUsername.Size = new System.Drawing.Size(60, 15);
            this.lblRegistrationUsername.TabIndex = 5;
            this.lblRegistrationUsername.Text = "Username";
            // 
            // tabUserAuthentication
            // 
            this.tabUserAuthentication.Controls.Add(this.chklAuthenticationMediators);
            this.tabUserAuthentication.Controls.Add(this.txtAuthenticationUsername);
            this.tabUserAuthentication.Controls.Add(this.btnAuthenticationClear);
            this.tabUserAuthentication.Controls.Add(this.txtAuthenticationPassword);
            this.tabUserAuthentication.Controls.Add(this.lblAuthenticationPassword);
            this.tabUserAuthentication.Controls.Add(this.btnAuthenticationSend);
            this.tabUserAuthentication.Controls.Add(this.lblUserAuthentication);
            this.tabUserAuthentication.Location = new System.Drawing.Point(4, 24);
            this.tabUserAuthentication.Name = "tabUserAuthentication";
            this.tabUserAuthentication.Padding = new System.Windows.Forms.Padding(3);
            this.tabUserAuthentication.Size = new System.Drawing.Size(493, 118);
            this.tabUserAuthentication.TabIndex = 1;
            this.tabUserAuthentication.Text = "User Authentication";
            this.tabUserAuthentication.UseVisualStyleBackColor = true;
            // 
            // chklAuthenticationMediators
            // 
            this.chklAuthenticationMediators.FormattingEnabled = true;
            this.chklAuthenticationMediators.Location = new System.Drawing.Point(220, 16);
            this.chklAuthenticationMediators.Name = "chklAuthenticationMediators";
            this.chklAuthenticationMediators.Size = new System.Drawing.Size(120, 76);
            this.chklAuthenticationMediators.TabIndex = 13;
            // 
            // txtAuthenticationUsername
            // 
            this.txtAuthenticationUsername.FormattingEnabled = true;
            this.txtAuthenticationUsername.Location = new System.Drawing.Point(84, 16);
            this.txtAuthenticationUsername.Name = "txtAuthenticationUsername";
            this.txtAuthenticationUsername.Size = new System.Drawing.Size(100, 23);
            this.txtAuthenticationUsername.TabIndex = 12;
            // 
            // btnAuthenticationClear
            // 
            this.btnAuthenticationClear.Location = new System.Drawing.Point(401, 73);
            this.btnAuthenticationClear.Name = "btnAuthenticationClear";
            this.btnAuthenticationClear.Size = new System.Drawing.Size(84, 23);
            this.btnAuthenticationClear.TabIndex = 11;
            this.btnAuthenticationClear.Text = "Clear";
            this.btnAuthenticationClear.UseVisualStyleBackColor = true;
            this.btnAuthenticationClear.Click += new System.EventHandler(this.BtnAuthenticationClear_Click);
            // 
            // txtAuthenticationPassword
            // 
            this.txtAuthenticationPassword.Location = new System.Drawing.Point(84, 44);
            this.txtAuthenticationPassword.Name = "txtAuthenticationPassword";
            this.txtAuthenticationPassword.Size = new System.Drawing.Size(100, 23);
            this.txtAuthenticationPassword.TabIndex = 4;
            // 
            // lblAuthenticationPassword
            // 
            this.lblAuthenticationPassword.AutoSize = true;
            this.lblAuthenticationPassword.Location = new System.Drawing.Point(18, 47);
            this.lblAuthenticationPassword.Name = "lblAuthenticationPassword";
            this.lblAuthenticationPassword.Size = new System.Drawing.Size(57, 15);
            this.lblAuthenticationPassword.TabIndex = 3;
            this.lblAuthenticationPassword.Text = "Password";
            // 
            // btnAuthenticationSend
            // 
            this.btnAuthenticationSend.Location = new System.Drawing.Point(18, 73);
            this.btnAuthenticationSend.Name = "btnAuthenticationSend";
            this.btnAuthenticationSend.Size = new System.Drawing.Size(166, 23);
            this.btnAuthenticationSend.TabIndex = 1;
            this.btnAuthenticationSend.Text = "Send";
            this.btnAuthenticationSend.UseVisualStyleBackColor = true;
            this.btnAuthenticationSend.Click += new System.EventHandler(this.BtnAuthenticationSend_Click);
            // 
            // lblUserAuthentication
            // 
            this.lblUserAuthentication.AutoSize = true;
            this.lblUserAuthentication.Location = new System.Drawing.Point(18, 19);
            this.lblUserAuthentication.Name = "lblUserAuthentication";
            this.lblUserAuthentication.Size = new System.Drawing.Size(60, 15);
            this.lblUserAuthentication.TabIndex = 0;
            this.lblUserAuthentication.Text = "Username";
            // 
            // tabMediatorRegistration
            // 
            this.tabMediatorRegistration.Controls.Add(this.cboAlgoritm);
            this.tabMediatorRegistration.Controls.Add(this.lblAlgorithm);
            this.tabMediatorRegistration.Controls.Add(this.nudExpiration);
            this.tabMediatorRegistration.Controls.Add(this.btnMediatorClear);
            this.tabMediatorRegistration.Controls.Add(this.lblExpiration);
            this.tabMediatorRegistration.Controls.Add(this.txtMediatorName);
            this.tabMediatorRegistration.Controls.Add(this.btnMediator);
            this.tabMediatorRegistration.Controls.Add(this.lblMediatorName);
            this.tabMediatorRegistration.Location = new System.Drawing.Point(4, 24);
            this.tabMediatorRegistration.Name = "tabMediatorRegistration";
            this.tabMediatorRegistration.Padding = new System.Windows.Forms.Padding(3);
            this.tabMediatorRegistration.Size = new System.Drawing.Size(493, 118);
            this.tabMediatorRegistration.TabIndex = 2;
            this.tabMediatorRegistration.Text = "Mediator Registration";
            this.tabMediatorRegistration.UseVisualStyleBackColor = true;
            // 
            // cboAlgoritm
            // 
            this.cboAlgoritm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlgoritm.FormattingEnabled = true;
            this.cboAlgoritm.Location = new System.Drawing.Point(328, 16);
            this.cboAlgoritm.Name = "cboAlgoritm";
            this.cboAlgoritm.Size = new System.Drawing.Size(104, 23);
            this.cboAlgoritm.TabIndex = 19;
            // 
            // lblAlgorithm
            // 
            this.lblAlgorithm.AutoSize = true;
            this.lblAlgorithm.Location = new System.Drawing.Point(261, 19);
            this.lblAlgorithm.Name = "lblAlgorithm";
            this.lblAlgorithm.Size = new System.Drawing.Size(61, 15);
            this.lblAlgorithm.TabIndex = 18;
            this.lblAlgorithm.Text = "Algorithm";
            // 
            // nudExpiration
            // 
            this.nudExpiration.Location = new System.Drawing.Point(191, 45);
            this.nudExpiration.Name = "nudExpiration";
            this.nudExpiration.Size = new System.Drawing.Size(46, 23);
            this.nudExpiration.TabIndex = 17;
            // 
            // btnMediatorClear
            // 
            this.btnMediatorClear.Location = new System.Drawing.Point(401, 73);
            this.btnMediatorClear.Name = "btnMediatorClear";
            this.btnMediatorClear.Size = new System.Drawing.Size(84, 23);
            this.btnMediatorClear.TabIndex = 16;
            this.btnMediatorClear.Text = "Clear";
            this.btnMediatorClear.UseVisualStyleBackColor = true;
            this.btnMediatorClear.Click += new System.EventHandler(this.btnMediatorClear_Click);
            // 
            // lblExpiration
            // 
            this.lblExpiration.AutoSize = true;
            this.lblExpiration.Location = new System.Drawing.Point(18, 47);
            this.lblExpiration.Name = "lblExpiration";
            this.lblExpiration.Size = new System.Drawing.Size(172, 15);
            this.lblExpiration.TabIndex = 14;
            this.lblExpiration.Text = "JWT Expiration Time In Minutes";
            // 
            // txtMediatorName
            // 
            this.txtMediatorName.Location = new System.Drawing.Point(109, 16);
            this.txtMediatorName.Name = "txtMediatorName";
            this.txtMediatorName.Size = new System.Drawing.Size(128, 23);
            this.txtMediatorName.TabIndex = 13;
            // 
            // btnMediator
            // 
            this.btnMediator.Location = new System.Drawing.Point(18, 73);
            this.btnMediator.Name = "btnMediator";
            this.btnMediator.Size = new System.Drawing.Size(219, 23);
            this.btnMediator.TabIndex = 12;
            this.btnMediator.Text = "Send";
            this.btnMediator.UseVisualStyleBackColor = true;
            this.btnMediator.Click += new System.EventHandler(this.btnMediator_Click);
            // 
            // lblMediatorName
            // 
            this.lblMediatorName.AutoSize = true;
            this.lblMediatorName.Location = new System.Drawing.Point(18, 19);
            this.lblMediatorName.Name = "lblMediatorName";
            this.lblMediatorName.Size = new System.Drawing.Size(90, 15);
            this.lblMediatorName.TabIndex = 11;
            this.lblMediatorName.Text = "Mediator Name";
            // 
            // tabMediatorAuthentication
            // 
            this.tabMediatorAuthentication.Controls.Add(this.lblMediatorAuthenticationClear);
            this.tabMediatorAuthentication.Controls.Add(this.btnMediatorAuthentication);
            this.tabMediatorAuthentication.Controls.Add(this.cboMediatorAuthenticationMediatorName);
            this.tabMediatorAuthentication.Controls.Add(this.lblMediatorAuthenticationMediatorName);
            this.tabMediatorAuthentication.Controls.Add(this.cboMediatorAuthenticationUsername);
            this.tabMediatorAuthentication.Controls.Add(this.lblMediatorAuthenticationUsername);
            this.tabMediatorAuthentication.Location = new System.Drawing.Point(4, 24);
            this.tabMediatorAuthentication.Name = "tabMediatorAuthentication";
            this.tabMediatorAuthentication.Padding = new System.Windows.Forms.Padding(3);
            this.tabMediatorAuthentication.Size = new System.Drawing.Size(493, 118);
            this.tabMediatorAuthentication.TabIndex = 3;
            this.tabMediatorAuthentication.Text = "Mediator Authentication";
            this.tabMediatorAuthentication.UseVisualStyleBackColor = true;
            // 
            // cboMediatorAuthenticationUsername
            // 
            this.cboMediatorAuthenticationUsername.FormattingEnabled = true;
            this.cboMediatorAuthenticationUsername.Location = new System.Drawing.Point(84, 16);
            this.cboMediatorAuthenticationUsername.Name = "cboMediatorAuthenticationUsername";
            this.cboMediatorAuthenticationUsername.Size = new System.Drawing.Size(100, 23);
            this.cboMediatorAuthenticationUsername.TabIndex = 14;
            // 
            // lblMediatorAuthenticationUsername
            // 
            this.lblMediatorAuthenticationUsername.AutoSize = true;
            this.lblMediatorAuthenticationUsername.Location = new System.Drawing.Point(18, 19);
            this.lblMediatorAuthenticationUsername.Name = "lblMediatorAuthenticationUsername";
            this.lblMediatorAuthenticationUsername.Size = new System.Drawing.Size(60, 15);
            this.lblMediatorAuthenticationUsername.TabIndex = 13;
            this.lblMediatorAuthenticationUsername.Text = "Username";
            // 
            // cboMediatorAuthenticationMediatorName
            // 
            this.cboMediatorAuthenticationMediatorName.FormattingEnabled = true;
            this.cboMediatorAuthenticationMediatorName.Location = new System.Drawing.Point(84, 44);
            this.cboMediatorAuthenticationMediatorName.Name = "cboMediatorAuthenticationMediatorName";
            this.cboMediatorAuthenticationMediatorName.Size = new System.Drawing.Size(100, 23);
            this.cboMediatorAuthenticationMediatorName.TabIndex = 16;
            // 
            // lblMediatorAuthenticationMediatorName
            // 
            this.lblMediatorAuthenticationMediatorName.AutoSize = true;
            this.lblMediatorAuthenticationMediatorName.Location = new System.Drawing.Point(18, 47);
            this.lblMediatorAuthenticationMediatorName.Name = "lblMediatorAuthenticationMediatorName";
            this.lblMediatorAuthenticationMediatorName.Size = new System.Drawing.Size(55, 15);
            this.lblMediatorAuthenticationMediatorName.TabIndex = 15;
            this.lblMediatorAuthenticationMediatorName.Text = "Mediator";
            // 
            // btnMediatorAuthentication
            // 
            this.btnMediatorAuthentication.Location = new System.Drawing.Point(18, 73);
            this.btnMediatorAuthentication.Name = "btnMediatorAuthentication";
            this.btnMediatorAuthentication.Size = new System.Drawing.Size(166, 23);
            this.btnMediatorAuthentication.TabIndex = 17;
            this.btnMediatorAuthentication.Text = "Send";
            this.btnMediatorAuthentication.UseVisualStyleBackColor = true;
            this.btnMediatorAuthentication.Click += new System.EventHandler(this.btnMediatorAuthentication_Click);
            // 
            // lblMediatorAuthenticationClear
            // 
            this.lblMediatorAuthenticationClear.Location = new System.Drawing.Point(401, 73);
            this.lblMediatorAuthenticationClear.Name = "lblMediatorAuthenticationClear";
            this.lblMediatorAuthenticationClear.Size = new System.Drawing.Size(84, 23);
            this.lblMediatorAuthenticationClear.TabIndex = 18;
            this.lblMediatorAuthenticationClear.Text = "Clear";
            this.lblMediatorAuthenticationClear.UseVisualStyleBackColor = true;
            this.lblMediatorAuthenticationClear.Click += new System.EventHandler(this.lblMediatorAuthenticationClear_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 146);
            this.Controls.Add(this.tabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmMain";
            this.Text = "JWT Authentication Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabMain.ResumeLayout(false);
            this.tabUserRegistration.ResumeLayout(false);
            this.tabUserRegistration.PerformLayout();
            this.tabUserAuthentication.ResumeLayout(false);
            this.tabUserAuthentication.PerformLayout();
            this.tabMediatorRegistration.ResumeLayout(false);
            this.tabMediatorRegistration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExpiration)).EndInit();
            this.tabMediatorAuthentication.ResumeLayout(false);
            this.tabMediatorAuthentication.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabMain;
        private TabPage tabUserRegistration;
        private TextBox txtRegistrationPassword;
        private Label lblRegistrationPassword;
        private TextBox txtRegistrationUsername;
        private Button btnRegistrationSend;
        private Label lblRegistrationUsername;
        private TabPage tabUserAuthentication;
        private TextBox txtAuthenticationPassword;
        private Label lblAuthenticationPassword;
        private Button btnAuthenticationSend;
        private Label lblUserAuthentication;
        private TabPage tabMediatorRegistration;
        private TabPage tabMediatorAuthentication;
        private Button btnRegistrationClear;
        private Button btnAuthenticationClear;
        private NumericUpDown nudExpiration;
        private Button btnMediatorClear;
        private Label lblExpiration;
        private TextBox txtMediatorName;
        private Button btnMediator;
        private Label lblMediatorName;
        private ComboBox txtAuthenticationUsername;
        private CheckedListBox chklRegistrationMediators;
        private CheckedListBox chklAuthenticationMediators;
        private ComboBox cboAlgoritm;
        private Label lblAlgorithm;
        private Button lblMediatorAuthenticationClear;
        private Button btnMediatorAuthentication;
        private ComboBox cboMediatorAuthenticationMediatorName;
        private Label lblMediatorAuthenticationMediatorName;
        private ComboBox cboMediatorAuthenticationUsername;
        private Label lblMediatorAuthenticationUsername;
    }
}