namespace MSsqlTools
{
    partial class UpdatePWD
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpdatePWD = new System.Windows.Forms.Button();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(105, 65);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUpdatePWD
            // 
            this.btnUpdatePWD.Location = new System.Drawing.Point(24, 65);
            this.btnUpdatePWD.Name = "btnUpdatePWD";
            this.btnUpdatePWD.Size = new System.Drawing.Size(75, 23);
            this.btnUpdatePWD.TabIndex = 6;
            this.btnUpdatePWD.Text = "确定";
            this.btnUpdatePWD.UseVisualStyleBackColor = true;
            this.btnUpdatePWD.Click += new System.EventHandler(this.btnUpdatePWD_Click);
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(74, 19);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '#';
            this.txtPWD.Size = new System.Drawing.Size(100, 21);
            this.txtPWD.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "sa密码:";
            // 
            // UpdatePWD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 107);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdatePWD);
            this.Controls.Add(this.txtPWD);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdatePWD";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输入";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpdatePWD;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label label1;
    }
}