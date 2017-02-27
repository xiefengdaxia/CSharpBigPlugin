namespace MSsqlTools
{
    partial class RestoreDB
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
            this.btnOpenFileDialog = new System.Windows.Forms.Button();
            this.txtFromFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtToPath = new System.Windows.Forms.TextBox();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOpenFileDialog
            // 
            this.btnOpenFileDialog.Location = new System.Drawing.Point(335, 20);
            this.btnOpenFileDialog.Name = "btnOpenFileDialog";
            this.btnOpenFileDialog.Size = new System.Drawing.Size(32, 23);
            this.btnOpenFileDialog.TabIndex = 9;
            this.btnOpenFileDialog.Text = "...";
            this.btnOpenFileDialog.UseVisualStyleBackColor = true;
            this.btnOpenFileDialog.Click += new System.EventHandler(this.btnOpenFileDialog_Click);
            // 
            // txtFromFile
            // 
            this.txtFromFile.Location = new System.Drawing.Point(74, 22);
            this.txtFromFile.Name = "txtFromFile";
            this.txtFromFile.Size = new System.Drawing.Size(255, 21);
            this.txtFromFile.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "从文件:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(292, 137);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(74, 137);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(75, 23);
            this.btnBackup.TabIndex = 6;
            this.btnBackup.Text = "还原";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "到路径:";
            // 
            // txtToPath
            // 
            this.txtToPath.Location = new System.Drawing.Point(74, 59);
            this.txtToPath.Name = "txtToPath";
            this.txtToPath.Size = new System.Drawing.Size(255, 21);
            this.txtToPath.TabIndex = 8;
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Location = new System.Drawing.Point(335, 57);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(32, 23);
            this.btnChooseFolder.TabIndex = 9;
            this.btnChooseFolder.Text = "...";
            this.btnChooseFolder.UseVisualStyleBackColor = true;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "文件名:";
            // 
            // txtReName
            // 
            this.txtReName.Location = new System.Drawing.Point(74, 99);
            this.txtReName.Name = "txtReName";
            this.txtReName.Size = new System.Drawing.Size(100, 21);
            this.txtReName.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(180, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "后缀自带*.MDF|*_LOG.LDF";
            // 
            // RestoreDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 172);
            this.Controls.Add(this.txtReName);
            this.Controls.Add(this.btnChooseFolder);
            this.Controls.Add(this.btnOpenFileDialog);
            this.Controls.Add(this.txtToPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFromFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnBackup);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RestoreDB";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "还原数据库";
            this.Load += new System.EventHandler(this.RestoreDB_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFileDialog;
        private System.Windows.Forms.TextBox txtFromFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtToPath;
        private System.Windows.Forms.Button btnChooseFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReName;
        private System.Windows.Forms.Label label4;
    }
}