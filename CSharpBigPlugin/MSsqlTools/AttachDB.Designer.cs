namespace MSsqlTools
{
    partial class AttachDB
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
            this.btnOpenFileDialogForMdf = new System.Windows.Forms.Button();
            this.txtMDFPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAttach = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLDFPath = new System.Windows.Forms.TextBox();
            this.btnOpenFileDialogForLdf = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRename = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOpenFileDialogForMdf
            // 
            this.btnOpenFileDialogForMdf.Location = new System.Drawing.Point(344, 12);
            this.btnOpenFileDialogForMdf.Name = "btnOpenFileDialogForMdf";
            this.btnOpenFileDialogForMdf.Size = new System.Drawing.Size(32, 23);
            this.btnOpenFileDialogForMdf.TabIndex = 9;
            this.btnOpenFileDialogForMdf.Text = "...";
            this.btnOpenFileDialogForMdf.UseVisualStyleBackColor = true;
            this.btnOpenFileDialogForMdf.Click += new System.EventHandler(this.btnOpenFileDialogForMdf_Click);
            // 
            // txtMDFPath
            // 
            this.txtMDFPath.Location = new System.Drawing.Point(73, 12);
            this.txtMDFPath.Name = "txtMDFPath";
            this.txtMDFPath.Size = new System.Drawing.Size(265, 21);
            this.txtMDFPath.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "MDF文件:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(301, 82);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAttach
            // 
            this.btnAttach.Location = new System.Drawing.Point(202, 82);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(75, 23);
            this.btnAttach.TabIndex = 6;
            this.btnAttach.Text = "附加";
            this.btnAttach.UseVisualStyleBackColor = true;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "LDF文件:";
            // 
            // txtLDFPath
            // 
            this.txtLDFPath.Location = new System.Drawing.Point(73, 48);
            this.txtLDFPath.Name = "txtLDFPath";
            this.txtLDFPath.Size = new System.Drawing.Size(265, 21);
            this.txtLDFPath.TabIndex = 8;
            // 
            // btnOpenFileDialogForLdf
            // 
            this.btnOpenFileDialogForLdf.Location = new System.Drawing.Point(344, 46);
            this.btnOpenFileDialogForLdf.Name = "btnOpenFileDialogForLdf";
            this.btnOpenFileDialogForLdf.Size = new System.Drawing.Size(32, 23);
            this.btnOpenFileDialogForLdf.TabIndex = 9;
            this.btnOpenFileDialogForLdf.Text = "...";
            this.btnOpenFileDialogForLdf.UseVisualStyleBackColor = true;
            this.btnOpenFileDialogForLdf.Click += new System.EventHandler(this.btnOpenFileDialogForLdf_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "新库名:";
            // 
            // txtRename
            // 
            this.txtRename.Location = new System.Drawing.Point(73, 82);
            this.txtRename.Name = "txtRename";
            this.txtRename.Size = new System.Drawing.Size(112, 21);
            this.txtRename.TabIndex = 10;
            // 
            // AttachDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 124);
            this.Controls.Add(this.txtRename);
            this.Controls.Add(this.btnOpenFileDialogForLdf);
            this.Controls.Add(this.btnOpenFileDialogForMdf);
            this.Controls.Add(this.txtLDFPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMDFPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAttach);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AttachDB";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "附加数据库";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFileDialogForMdf;
        private System.Windows.Forms.TextBox txtMDFPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLDFPath;
        private System.Windows.Forms.Button btnOpenFileDialogForLdf;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRename;
    }
}