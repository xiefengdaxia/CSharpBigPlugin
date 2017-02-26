namespace MSsqlTools
{
    partial class UpdatePort
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnUpdatePort = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口号:";
            // 
            // textBox1
            // 
            this.txtPort.Location = new System.Drawing.Point(74, 26);
            this.txtPort.Name = "textBox1";
            this.txtPort.Size = new System.Drawing.Size(100, 21);
            this.txtPort.TabIndex = 1;
            // 
            // btnUpdatePort
            // 
            this.btnUpdatePort.Location = new System.Drawing.Point(24, 72);
            this.btnUpdatePort.Name = "btnUpdatePort";
            this.btnUpdatePort.Size = new System.Drawing.Size(75, 23);
            this.btnUpdatePort.TabIndex = 2;
            this.btnUpdatePort.Text = "确定";
            this.btnUpdatePort.UseVisualStyleBackColor = true;
            this.btnUpdatePort.Click += new System.EventHandler(this.btnUpdatePort_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(105, 72);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // UpdatePort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 107);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdatePort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdatePort";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输入";
            this.Load += new System.EventHandler(this.UpdatePort_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnUpdatePort;
        private System.Windows.Forms.Button btnClose;
    }
}