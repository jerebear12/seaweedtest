namespace SeaweedTestMk2
{
    partial class formLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formLogin));
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.pnlUsername = new System.Windows.Forms.Panel();
            this.pnlPassword = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lnkSignUp = new System.Windows.Forms.LinkLabel();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.lblBottom = new System.Windows.Forms.Label();
            this.pnlSignUp = new System.Windows.Forms.Panel();
            this.txtSUPassword = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblFooter = new System.Windows.Forms.Label();
            this.lblSignUp = new System.Windows.Forms.Label();
            this.lnkLogin = new System.Windows.Forms.LinkLabel();
            this.txtSUUsername = new System.Windows.Forms.TextBox();
            this.btnSUClear = new System.Windows.Forms.Button();
            this.pnlDividerTop = new System.Windows.Forms.Panel();
            this.btnSignUp = new System.Windows.Forms.Button();
            this.txtSUEmail = new System.Windows.Forms.TextBox();
            this.pnlDividerBot = new System.Windows.Forms.Panel();
            this.lblError = new System.Windows.Forms.Label();
            this.pnlError = new System.Windows.Forms.Panel();
            this.pnlSuccess = new System.Windows.Forms.Panel();
            this.lblSuccess = new System.Windows.Forms.Label();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lblLoadingSubHeader = new System.Windows.Forms.Label();
            this.lblLoadingHeader = new System.Windows.Forms.Label();
            this.pbLoading = new System.Windows.Forms.ProgressBar();
            this.btnSettings = new System.Windows.Forms.Button();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.btnCloseSettings = new System.Windows.Forms.Button();
            this.btnClearSettings = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFilerIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAPI = new System.Windows.Forms.Label();
            this.txtAPIIP = new System.Windows.Forms.TextBox();
            this.lblSetHeader = new System.Windows.Forms.Label();
            this.pnlLogin.SuspendLayout();
            this.pnlSignUp.SuspendLayout();
            this.pnlError.SuspendLayout();
            this.pnlSuccess.SuspendLayout();
            this.pnlLoading.SuspendLayout();
            this.pnlSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLogin.Location = new System.Drawing.Point(105, 22);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(56, 21);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Text = "Log In";
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.White;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Location = new System.Drawing.Point(9, 111);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(248, 16);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Text = "Username";
            this.txtUsername.Enter += new System.EventHandler(this.txtUsername_Enter);
            // 
            // pnlUsername
            // 
            this.pnlUsername.BackColor = System.Drawing.Color.DarkGray;
            this.pnlUsername.Location = new System.Drawing.Point(9, 129);
            this.pnlUsername.Name = "pnlUsername";
            this.pnlUsername.Size = new System.Drawing.Size(248, 3);
            this.pnlUsername.TabIndex = 3;
            // 
            // pnlPassword
            // 
            this.pnlPassword.BackColor = System.Drawing.Color.DarkGray;
            this.pnlPassword.Location = new System.Drawing.Point(9, 182);
            this.pnlPassword.Name = "pnlPassword";
            this.pnlPassword.Size = new System.Drawing.Size(248, 3);
            this.pnlPassword.TabIndex = 5;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Location = new System.Drawing.Point(9, 164);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(248, 16);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "Password";
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnLogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogIn.ForeColor = System.Drawing.Color.White;
            this.btnLogIn.Location = new System.Drawing.Point(137, 232);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(120, 29);
            this.btnLogIn.TabIndex = 4;
            this.btnLogIn.Text = "Login";
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(9, 232);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 29);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lnkSignUp
            // 
            this.lnkSignUp.AutoSize = true;
            this.lnkSignUp.Location = new System.Drawing.Point(157, 332);
            this.lnkSignUp.Name = "lnkSignUp";
            this.lnkSignUp.Size = new System.Drawing.Size(48, 15);
            this.lnkSignUp.TabIndex = 5;
            this.lnkSignUp.TabStop = true;
            this.lnkSignUp.Text = "Sign Up";
            this.lnkSignUp.VisitedLinkColor = System.Drawing.Color.Cyan;
            this.lnkSignUp.Click += new System.EventHandler(this.lnkSignUp_Click);
            // 
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.lblBottom);
            this.pnlLogin.Controls.Add(this.lblLogin);
            this.pnlLogin.Controls.Add(this.lnkSignUp);
            this.pnlLogin.Controls.Add(this.txtUsername);
            this.pnlLogin.Controls.Add(this.btnClear);
            this.pnlLogin.Controls.Add(this.pnlUsername);
            this.pnlLogin.Controls.Add(this.btnLogIn);
            this.pnlLogin.Controls.Add(this.txtPassword);
            this.pnlLogin.Controls.Add(this.pnlPassword);
            this.pnlLogin.Location = new System.Drawing.Point(12, 12);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(270, 370);
            this.pnlLogin.TabIndex = 6;
            this.pnlLogin.Click += new System.EventHandler(this.pnlLogin_Click);
            // 
            // lblBottom
            // 
            this.lblBottom.AutoSize = true;
            this.lblBottom.Location = new System.Drawing.Point(55, 332);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(102, 15);
            this.lblBottom.TabIndex = 15;
            this.lblBottom.Text = "Need an account?";
            // 
            // pnlSignUp
            // 
            this.pnlSignUp.Controls.Add(this.txtSUPassword);
            this.pnlSignUp.Controls.Add(this.panel4);
            this.pnlSignUp.Controls.Add(this.lblFooter);
            this.pnlSignUp.Controls.Add(this.lblSignUp);
            this.pnlSignUp.Controls.Add(this.lnkLogin);
            this.pnlSignUp.Controls.Add(this.txtSUUsername);
            this.pnlSignUp.Controls.Add(this.btnSUClear);
            this.pnlSignUp.Controls.Add(this.pnlDividerTop);
            this.pnlSignUp.Controls.Add(this.btnSignUp);
            this.pnlSignUp.Controls.Add(this.txtSUEmail);
            this.pnlSignUp.Controls.Add(this.pnlDividerBot);
            this.pnlSignUp.Location = new System.Drawing.Point(12, 11);
            this.pnlSignUp.Name = "pnlSignUp";
            this.pnlSignUp.Size = new System.Drawing.Size(270, 370);
            this.pnlSignUp.TabIndex = 7;
            this.pnlSignUp.Click += new System.EventHandler(this.pnlSignUp_Click);
            // 
            // txtSUPassword
            // 
            this.txtSUPassword.BackColor = System.Drawing.Color.White;
            this.txtSUPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSUPassword.Location = new System.Drawing.Point(9, 218);
            this.txtSUPassword.Name = "txtSUPassword";
            this.txtSUPassword.Size = new System.Drawing.Size(248, 16);
            this.txtSUPassword.TabIndex = 15;
            this.txtSUPassword.Text = "Password";
            this.txtSUPassword.Enter += new System.EventHandler(this.txtSUPassword_Enter);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkGray;
            this.panel4.Location = new System.Drawing.Point(9, 236);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(248, 3);
            this.panel4.TabIndex = 16;
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Location = new System.Drawing.Point(33, 333);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(142, 15);
            this.lblFooter.TabIndex = 14;
            this.lblFooter.Text = "Already have an account?";
            // 
            // lblSignUp
            // 
            this.lblSignUp.AutoSize = true;
            this.lblSignUp.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSignUp.Location = new System.Drawing.Point(105, 22);
            this.lblSignUp.Name = "lblSignUp";
            this.lblSignUp.Size = new System.Drawing.Size(67, 21);
            this.lblSignUp.TabIndex = 6;
            this.lblSignUp.Text = "Sign Up";
            // 
            // lnkLogin
            // 
            this.lnkLogin.AutoSize = true;
            this.lnkLogin.Location = new System.Drawing.Point(178, 333);
            this.lnkLogin.Name = "lnkLogin";
            this.lnkLogin.Size = new System.Drawing.Size(37, 15);
            this.lnkLogin.TabIndex = 12;
            this.lnkLogin.TabStop = true;
            this.lnkLogin.Text = "Login";
            this.lnkLogin.VisitedLinkColor = System.Drawing.Color.Cyan;
            this.lnkLogin.Click += new System.EventHandler(this.lnkLogin_Click);
            // 
            // txtSUUsername
            // 
            this.txtSUUsername.BackColor = System.Drawing.Color.White;
            this.txtSUUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSUUsername.Location = new System.Drawing.Point(9, 111);
            this.txtSUUsername.Name = "txtSUUsername";
            this.txtSUUsername.Size = new System.Drawing.Size(248, 16);
            this.txtSUUsername.TabIndex = 7;
            this.txtSUUsername.Text = "Username";
            this.txtSUUsername.Enter += new System.EventHandler(this.txtSUUsername_Enter);
            // 
            // btnSUClear
            // 
            this.btnSUClear.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnSUClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSUClear.ForeColor = System.Drawing.Color.White;
            this.btnSUClear.Location = new System.Drawing.Point(9, 285);
            this.btnSUClear.Name = "btnSUClear";
            this.btnSUClear.Size = new System.Drawing.Size(120, 29);
            this.btnSUClear.TabIndex = 9;
            this.btnSUClear.Text = "Clear";
            this.btnSUClear.UseVisualStyleBackColor = false;
            this.btnSUClear.Click += new System.EventHandler(this.btnSUClear_Click);
            // 
            // pnlDividerTop
            // 
            this.pnlDividerTop.BackColor = System.Drawing.Color.DarkGray;
            this.pnlDividerTop.Location = new System.Drawing.Point(9, 129);
            this.pnlDividerTop.Name = "pnlDividerTop";
            this.pnlDividerTop.Size = new System.Drawing.Size(248, 3);
            this.pnlDividerTop.TabIndex = 10;
            // 
            // btnSignUp
            // 
            this.btnSignUp.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSignUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignUp.ForeColor = System.Drawing.Color.White;
            this.btnSignUp.Location = new System.Drawing.Point(135, 285);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(120, 29);
            this.btnSignUp.TabIndex = 11;
            this.btnSignUp.Text = "Sign Up";
            this.btnSignUp.UseVisualStyleBackColor = false;
            this.btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click);
            // 
            // txtSUEmail
            // 
            this.txtSUEmail.BackColor = System.Drawing.Color.White;
            this.txtSUEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSUEmail.Location = new System.Drawing.Point(9, 165);
            this.txtSUEmail.Name = "txtSUEmail";
            this.txtSUEmail.Size = new System.Drawing.Size(248, 16);
            this.txtSUEmail.TabIndex = 8;
            this.txtSUEmail.Text = "Email";
            this.txtSUEmail.Enter += new System.EventHandler(this.txtSUEmail_Enter);
            // 
            // pnlDividerBot
            // 
            this.pnlDividerBot.BackColor = System.Drawing.Color.DarkGray;
            this.pnlDividerBot.Location = new System.Drawing.Point(9, 183);
            this.pnlDividerBot.Name = "pnlDividerBot";
            this.pnlDividerBot.Size = new System.Drawing.Size(248, 3);
            this.pnlDividerBot.TabIndex = 13;
            // 
            // lblError
            // 
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            this.lblError.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblError.ForeColor = System.Drawing.Color.White;
            this.lblError.Location = new System.Drawing.Point(15, 4);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(160, 22);
            this.lblError.TabIndex = 16;
            // 
            // pnlError
            // 
            this.pnlError.BackColor = System.Drawing.Color.IndianRed;
            this.pnlError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlError.Controls.Add(this.lblError);
            this.pnlError.Location = new System.Drawing.Point(53, 75);
            this.pnlError.Name = "pnlError";
            this.pnlError.Size = new System.Drawing.Size(191, 29);
            this.pnlError.TabIndex = 17;
            // 
            // pnlSuccess
            // 
            this.pnlSuccess.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.pnlSuccess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSuccess.Controls.Add(this.lblSuccess);
            this.pnlSuccess.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlSuccess.Location = new System.Drawing.Point(53, 75);
            this.pnlSuccess.Name = "pnlSuccess";
            this.pnlSuccess.Size = new System.Drawing.Size(191, 29);
            this.pnlSuccess.TabIndex = 18;
            // 
            // lblSuccess
            // 
            this.lblSuccess.BackColor = System.Drawing.Color.Transparent;
            this.lblSuccess.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSuccess.ForeColor = System.Drawing.Color.White;
            this.lblSuccess.Location = new System.Drawing.Point(15, 4);
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(160, 21);
            this.lblSuccess.TabIndex = 16;
            this.lblSuccess.Text = "Successful Login";
            this.lblSuccess.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlLoading
            // 
            this.pnlLoading.Controls.Add(this.lblLoadingSubHeader);
            this.pnlLoading.Controls.Add(this.lblLoadingHeader);
            this.pnlLoading.Controls.Add(this.pbLoading);
            this.pnlLoading.Location = new System.Drawing.Point(13, 11);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(269, 371);
            this.pnlLoading.TabIndex = 19;
            // 
            // lblLoadingSubHeader
            // 
            this.lblLoadingSubHeader.AutoSize = true;
            this.lblLoadingSubHeader.Location = new System.Drawing.Point(105, 77);
            this.lblLoadingSubHeader.Name = "lblLoadingSubHeader";
            this.lblLoadingSubHeader.Size = new System.Drawing.Size(59, 15);
            this.lblLoadingSubHeader.TabIndex = 1;
            this.lblLoadingSubHeader.Text = "Loading...";
            // 
            // lblLoadingHeader
            // 
            this.lblLoadingHeader.AutoSize = true;
            this.lblLoadingHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLoadingHeader.Location = new System.Drawing.Point(68, 43);
            this.lblLoadingHeader.Name = "lblLoadingHeader";
            this.lblLoadingHeader.Size = new System.Drawing.Size(132, 21);
            this.lblLoadingHeader.TabIndex = 0;
            this.lblLoadingHeader.Text = "SeaweedFS Tool";
            // 
            // pbLoading
            // 
            this.pbLoading.ForeColor = System.Drawing.Color.SeaGreen;
            this.pbLoading.Location = new System.Drawing.Point(64, 233);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(140, 23);
            this.pbLoading.TabIndex = 2;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.BackgroundImage = global::SeaweedTestMk2.Properties.Resources.settings;
            this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSettings.Location = new System.Drawing.Point(262, 5);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(28, 26);
            this.btnSettings.TabIndex = 16;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.btnSaveSettings);
            this.pnlSettings.Controls.Add(this.btnCloseSettings);
            this.pnlSettings.Controls.Add(this.btnClearSettings);
            this.pnlSettings.Controls.Add(this.txtFolder);
            this.pnlSettings.Controls.Add(this.label2);
            this.pnlSettings.Controls.Add(this.txtFilerIP);
            this.pnlSettings.Controls.Add(this.label1);
            this.pnlSettings.Controls.Add(this.lblAPI);
            this.pnlSettings.Controls.Add(this.txtAPIIP);
            this.pnlSettings.Controls.Add(this.lblSetHeader);
            this.pnlSettings.Location = new System.Drawing.Point(12, 11);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(270, 371);
            this.pnlSettings.TabIndex = 20;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSettings.Location = new System.Drawing.Point(185, 344);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSettings.TabIndex = 9;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.UseVisualStyleBackColor = false;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // btnCloseSettings
            // 
            this.btnCloseSettings.BackColor = System.Drawing.Color.IndianRed;
            this.btnCloseSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseSettings.Location = new System.Drawing.Point(97, 344);
            this.btnCloseSettings.Name = "btnCloseSettings";
            this.btnCloseSettings.Size = new System.Drawing.Size(75, 23);
            this.btnCloseSettings.TabIndex = 8;
            this.btnCloseSettings.Text = "Close";
            this.btnCloseSettings.UseVisualStyleBackColor = false;
            this.btnCloseSettings.Click += new System.EventHandler(this.btnCloseSettings_Click);
            // 
            // btnClearSettings
            // 
            this.btnClearSettings.Location = new System.Drawing.Point(9, 344);
            this.btnClearSettings.Name = "btnClearSettings";
            this.btnClearSettings.Size = new System.Drawing.Size(75, 23);
            this.btnClearSettings.TabIndex = 7;
            this.btnClearSettings.Text = "Clear";
            this.btnClearSettings.UseVisualStyleBackColor = true;
            this.btnClearSettings.Click += new System.EventHandler(this.btnClearSettings_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(10, 207);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(250, 23);
            this.txtFolder.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Folder to Store Files In:";
            // 
            // txtFilerIP
            // 
            this.txtFilerIP.Location = new System.Drawing.Point(10, 151);
            this.txtFilerIP.Name = "txtFilerIP";
            this.txtFilerIP.Size = new System.Drawing.Size(250, 23);
            this.txtFilerIP.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filer IP Address:";
            // 
            // lblAPI
            // 
            this.lblAPI.AutoSize = true;
            this.lblAPI.Location = new System.Drawing.Point(13, 75);
            this.lblAPI.Name = "lblAPI";
            this.lblAPI.Size = new System.Drawing.Size(86, 15);
            this.lblAPI.TabIndex = 2;
            this.lblAPI.Text = "API IP Address:";
            // 
            // txtAPIIP
            // 
            this.txtAPIIP.Location = new System.Drawing.Point(10, 95);
            this.txtAPIIP.Name = "txtAPIIP";
            this.txtAPIIP.Size = new System.Drawing.Size(250, 23);
            this.txtAPIIP.TabIndex = 1;
            // 
            // lblSetHeader
            // 
            this.lblSetHeader.AutoSize = true;
            this.lblSetHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSetHeader.Location = new System.Drawing.Point(99, 12);
            this.lblSetHeader.Name = "lblSetHeader";
            this.lblSetHeader.Size = new System.Drawing.Size(72, 21);
            this.lblSetHeader.TabIndex = 0;
            this.lblSetHeader.Text = "Settings";
            // 
            // formLogin
            // 
            this.AcceptButton = this.btnLogIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(296, 410);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.pnlSignUp);
            this.Controls.Add(this.pnlSettings);
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.pnlSuccess);
            this.Controls.Add(this.pnlError);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formLogin";
            this.Text = "Login";
            this.Activated += new System.EventHandler(this.formLogin_Activated);
            this.Load += new System.EventHandler(this.formLogin_Load);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlSignUp.ResumeLayout(false);
            this.pnlSignUp.PerformLayout();
            this.pnlError.ResumeLayout(false);
            this.pnlSuccess.ResumeLayout(false);
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblLogin;
        private TextBox txtUsername;
        private Panel pnlUsername;
        private Panel pnlPassword;
        private TextBox txtPassword;
        private Button btnLogIn;
        private Button btnClear;
        private LinkLabel lnkSignUp;
        private Panel pnlLogin;
        private Panel pnlSignUp;
        private Label lblSignUp;
        private LinkLabel lnkLogin;
        private TextBox txtSUUsername;
        private Button btnSUClear;
        private Panel pnlDividerTop;
        private Button btnSignUp;
        private TextBox txtSUEmail;
        private Panel pnlDividerBot;
        private Label lblFooter;
        private TextBox txtSUPassword;
        private Panel panel4;
        private Label lblBottom;
        private Label lblError;
        private Panel pnlError;
        private Panel pnlSuccess;
        private Label lblSuccess;
        private Panel pnlLoading;
        private ProgressBar pbLoading;
        private Label lblLoadingSubHeader;
        private Label lblLoadingHeader;
        private Button btnSettings;
        private Panel pnlSettings;
        private Button btnSaveSettings;
        private Button btnCloseSettings;
        private Button btnClearSettings;
        private TextBox txtFolder;
        private Label label2;
        private TextBox txtFilerIP;
        private Label label1;
        private Label lblAPI;
        private TextBox txtAPIIP;
        private Label lblSetHeader;
    }
}