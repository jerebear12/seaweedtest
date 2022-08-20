namespace SeaweedTestMk2
{
    partial class FileManager
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
            this.pnlNavigation = new System.Windows.Forms.Panel();
            this.btnUploadFiles = new System.Windows.Forms.Button();
            this.btnViewFiles = new System.Windows.Forms.Button();
            this.pnlUploadFiles = new System.Windows.Forms.Panel();
            this.btnPause = new System.Windows.Forms.Button();
            this.txtFileInfo = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.gbRadioButtons = new System.Windows.Forms.GroupBox();
            this.rbFile = new System.Windows.Forms.RadioButton();
            this.rbFolder = new System.Windows.Forms.RadioButton();
            this.lblUpHeader = new System.Windows.Forms.Label();
            this.ofdFile = new System.Windows.Forms.OpenFileDialog();
            this.fbdFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlSuccess = new System.Windows.Forms.Panel();
            this.lblSuccess = new System.Windows.Forms.Label();
            this.pnlError = new System.Windows.Forms.Panel();
            this.lblError = new System.Windows.Forms.Label();
            this.pnlViewFiles = new System.Windows.Forms.Panel();
            this.lvImages = new System.Windows.Forms.ListView();
            this.lblDownHeader = new System.Windows.Forms.Label();
            this.ilImages = new System.Windows.Forms.ImageList(this.components);
            this.pbDisplayImage = new System.Windows.Forms.PictureBox();
            this.pnlPicture = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbUpload = new System.Windows.Forms.ProgressBar();
            this.pnlNavigation.SuspendLayout();
            this.pnlUploadFiles.SuspendLayout();
            this.gbRadioButtons.SuspendLayout();
            this.pnlSuccess.SuspendLayout();
            this.pnlError.SuspendLayout();
            this.pnlViewFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDisplayImage)).BeginInit();
            this.pnlPicture.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNavigation
            // 
            this.pnlNavigation.Controls.Add(this.btnUploadFiles);
            this.pnlNavigation.Controls.Add(this.btnViewFiles);
            this.pnlNavigation.Location = new System.Drawing.Point(1, 572);
            this.pnlNavigation.Name = "pnlNavigation";
            this.pnlNavigation.Size = new System.Drawing.Size(348, 62);
            this.pnlNavigation.TabIndex = 1;
            // 
            // btnUploadFiles
            // 
            this.btnUploadFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadFiles.Location = new System.Drawing.Point(174, 0);
            this.btnUploadFiles.Name = "btnUploadFiles";
            this.btnUploadFiles.Size = new System.Drawing.Size(174, 62);
            this.btnUploadFiles.TabIndex = 1;
            this.btnUploadFiles.Text = "Upload Files";
            this.btnUploadFiles.UseVisualStyleBackColor = true;
            this.btnUploadFiles.Click += new System.EventHandler(this.btnUploadFiles_Click);
            // 
            // btnViewFiles
            // 
            this.btnViewFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewFiles.Location = new System.Drawing.Point(0, 0);
            this.btnViewFiles.Name = "btnViewFiles";
            this.btnViewFiles.Size = new System.Drawing.Size(174, 62);
            this.btnViewFiles.TabIndex = 0;
            this.btnViewFiles.Text = "View Files";
            this.btnViewFiles.UseVisualStyleBackColor = true;
            this.btnViewFiles.Click += new System.EventHandler(this.btnViewFiles_Click);
            // 
            // pnlUploadFiles
            // 
            this.pnlUploadFiles.Controls.Add(this.pbUpload);
            this.pnlUploadFiles.Controls.Add(this.btnPause);
            this.pnlUploadFiles.Controls.Add(this.txtFileInfo);
            this.pnlUploadFiles.Controls.Add(this.btnClear);
            this.pnlUploadFiles.Controls.Add(this.btnChoose);
            this.pnlUploadFiles.Controls.Add(this.gbRadioButtons);
            this.pnlUploadFiles.Controls.Add(this.lblUpHeader);
            this.pnlUploadFiles.Location = new System.Drawing.Point(1, 1);
            this.pnlUploadFiles.Name = "pnlUploadFiles";
            this.pnlUploadFiles.Size = new System.Drawing.Size(348, 565);
            this.pnlUploadFiles.TabIndex = 2;
            this.pnlUploadFiles.Click += new System.EventHandler(this.pnlUploadFiles_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.IndianRed;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.ForeColor = System.Drawing.Color.White;
            this.btnPause.Location = new System.Drawing.Point(141, 339);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 29);
            this.btnPause.TabIndex = 10;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // txtFileInfo
            // 
            this.txtFileInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileInfo.Location = new System.Drawing.Point(54, 306);
            this.txtFileInfo.Name = "txtFileInfo";
            this.txtFileInfo.ReadOnly = true;
            this.txtFileInfo.Size = new System.Drawing.Size(246, 23);
            this.txtFileInfo.TabIndex = 9;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(54, 338);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 29);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnChoose
            // 
            this.btnChoose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChoose.ForeColor = System.Drawing.Color.White;
            this.btnChoose.Location = new System.Drawing.Point(225, 339);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(75, 29);
            this.btnChoose.TabIndex = 7;
            this.btnChoose.Text = "Choose";
            this.btnChoose.UseVisualStyleBackColor = false;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // gbRadioButtons
            // 
            this.gbRadioButtons.Controls.Add(this.rbFile);
            this.gbRadioButtons.Controls.Add(this.rbFolder);
            this.gbRadioButtons.Location = new System.Drawing.Point(54, 214);
            this.gbRadioButtons.Name = "gbRadioButtons";
            this.gbRadioButtons.Size = new System.Drawing.Size(246, 82);
            this.gbRadioButtons.TabIndex = 6;
            this.gbRadioButtons.TabStop = false;
            this.gbRadioButtons.Text = "Select One:";
            // 
            // rbFile
            // 
            this.rbFile.AutoSize = true;
            this.rbFile.Location = new System.Drawing.Point(24, 25);
            this.rbFile.Name = "rbFile";
            this.rbFile.Size = new System.Drawing.Size(43, 19);
            this.rbFile.TabIndex = 1;
            this.rbFile.TabStop = true;
            this.rbFile.Text = "File";
            this.rbFile.UseVisualStyleBackColor = true;
            // 
            // rbFolder
            // 
            this.rbFolder.AutoSize = true;
            this.rbFolder.Location = new System.Drawing.Point(24, 51);
            this.rbFolder.Name = "rbFolder";
            this.rbFolder.Size = new System.Drawing.Size(58, 19);
            this.rbFolder.TabIndex = 2;
            this.rbFolder.TabStop = true;
            this.rbFolder.Text = "Folder";
            this.rbFolder.UseVisualStyleBackColor = true;
            // 
            // lblUpHeader
            // 
            this.lblUpHeader.AutoSize = true;
            this.lblUpHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUpHeader.Location = new System.Drawing.Point(105, 39);
            this.lblUpHeader.Name = "lblUpHeader";
            this.lblUpHeader.Size = new System.Drawing.Size(138, 21);
            this.lblUpHeader.TabIndex = 0;
            this.lblUpHeader.Text = "Upload Manager";
            // 
            // ofdFile
            // 
            this.ofdFile.Title = "Add File";
            // 
            // pnlSuccess
            // 
            this.pnlSuccess.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.pnlSuccess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSuccess.Controls.Add(this.lblSuccess);
            this.pnlSuccess.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlSuccess.Location = new System.Drawing.Point(78, 167);
            this.pnlSuccess.Name = "pnlSuccess";
            this.pnlSuccess.Size = new System.Drawing.Size(191, 29);
            this.pnlSuccess.TabIndex = 19;
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
            // pnlError
            // 
            this.pnlError.BackColor = System.Drawing.Color.IndianRed;
            this.pnlError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlError.Controls.Add(this.lblError);
            this.pnlError.Location = new System.Drawing.Point(77, 166);
            this.pnlError.Name = "pnlError";
            this.pnlError.Size = new System.Drawing.Size(191, 29);
            this.pnlError.TabIndex = 20;
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
            // pnlViewFiles
            // 
            this.pnlViewFiles.Controls.Add(this.lvImages);
            this.pnlViewFiles.Controls.Add(this.lblDownHeader);
            this.pnlViewFiles.Location = new System.Drawing.Point(0, 0);
            this.pnlViewFiles.Name = "pnlViewFiles";
            this.pnlViewFiles.Size = new System.Drawing.Size(348, 565);
            this.pnlViewFiles.TabIndex = 17;
            // 
            // lvImages
            // 
            this.lvImages.Location = new System.Drawing.Point(4, 104);
            this.lvImages.MultiSelect = false;
            this.lvImages.Name = "lvImages";
            this.lvImages.Size = new System.Drawing.Size(341, 462);
            this.lvImages.TabIndex = 2;
            this.lvImages.UseCompatibleStateImageBehavior = false;
            this.lvImages.Click += new System.EventHandler(this.lvImages_Click);
            // 
            // lblDownHeader
            // 
            this.lblDownHeader.AutoSize = true;
            this.lblDownHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDownHeader.Location = new System.Drawing.Point(131, 41);
            this.lblDownHeader.Name = "lblDownHeader";
            this.lblDownHeader.Size = new System.Drawing.Size(86, 21);
            this.lblDownHeader.TabIndex = 1;
            this.lblDownHeader.Text = "View Files";
            // 
            // ilImages
            // 
            this.ilImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ilImages.ImageSize = new System.Drawing.Size(16, 16);
            this.ilImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pbDisplayImage
            // 
            this.pbDisplayImage.Location = new System.Drawing.Point(3, 3);
            this.pbDisplayImage.Name = "pbDisplayImage";
            this.pbDisplayImage.Size = new System.Drawing.Size(319, 213);
            this.pbDisplayImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDisplayImage.TabIndex = 3;
            this.pbDisplayImage.TabStop = false;
            // 
            // pnlPicture
            // 
            this.pnlPicture.Controls.Add(this.btnClose);
            this.pnlPicture.Controls.Add(this.pbDisplayImage);
            this.pnlPicture.Location = new System.Drawing.Point(12, 114);
            this.pnlPicture.Name = "pnlPicture";
            this.pnlPicture.Size = new System.Drawing.Size(325, 254);
            this.pnlPicture.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(125, 226);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbUpload
            // 
            this.pbUpload.Location = new System.Drawing.Point(54, 374);
            this.pbUpload.Name = "pbUpload";
            this.pbUpload.Size = new System.Drawing.Size(246, 23);
            this.pbUpload.Step = 1;
            this.pbUpload.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbUpload.TabIndex = 11;
            this.pbUpload.Visible = false;
            // 
            // FileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 635);
            this.Controls.Add(this.pnlUploadFiles);
            this.Controls.Add(this.pnlViewFiles);
            this.Controls.Add(this.pnlPicture);
            this.Controls.Add(this.pnlError);
            this.Controls.Add(this.pnlSuccess);
            this.Controls.Add(this.pnlNavigation);
            this.MaximumSize = new System.Drawing.Size(365, 674);
            this.Name = "FileManager";
            this.Text = "FileManager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileManager_FormClosed);
            this.pnlNavigation.ResumeLayout(false);
            this.pnlUploadFiles.ResumeLayout(false);
            this.pnlUploadFiles.PerformLayout();
            this.gbRadioButtons.ResumeLayout(false);
            this.gbRadioButtons.PerformLayout();
            this.pnlSuccess.ResumeLayout(false);
            this.pnlError.ResumeLayout(false);
            this.pnlViewFiles.ResumeLayout(false);
            this.pnlViewFiles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDisplayImage)).EndInit();
            this.pnlPicture.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnlNavigation;
        private Button btnUploadFiles;
        private Button btnViewFiles;
        private Panel pnlUploadFiles;
        private GroupBox gbRadioButtons;
        private RadioButton rbFile;
        private RadioButton rbFolder;
        private Label lblUpHeader;
        private OpenFileDialog ofdFile;
        private FolderBrowserDialog fbdFolder;
        private Button btnChoose;
        private TextBox txtFileInfo;
        private Button btnClear;
        private Panel pnlSuccess;
        private Label lblSuccess;
        private Panel pnlError;
        private Label lblError;
        private Panel pnlViewFiles;
        private Label lblDownHeader;
        private ListView lvImages;
        private ImageList ilImages;
        private Panel pnlPicture;
        private Button btnClose;
        private PictureBox pbDisplayImage;
        private Button btnPause;
        private ProgressBar pbUpload;
    }
}