namespace BuildAndExecuteSQL
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.button22 = new System.Windows.Forms.Button();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.textBoxConnectionString = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownTotalRecords = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBatchSqlConnStr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectAndExecSql = new System.Windows.Forms.Button();
            this.lblPs = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalRecords)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(62, 131);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(100, 21);
            this.textBox12.TabIndex = 52;
            this.textBox12.Text = "pos_sales";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(278, 136);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 12);
            this.label19.TabIndex = 50;
            this.label19.Text = "计数:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(21, 136);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 12);
            this.label18.TabIndex = 51;
            this.label18.Text = "表名:";
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(177, 126);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(83, 31);
            this.button22.TabIndex = 49;
            this.button22.Text = "生成脚本";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click_1);
            // 
            // textBox11
            // 
            this.textBox11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox11.ForeColor = System.Drawing.Color.Black;
            this.textBox11.Location = new System.Drawing.Point(328, 132);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(188, 21);
            this.textBox11.TabIndex = 48;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(22, 36);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(494, 70);
            this.richTextBox2.TabIndex = 47;
            this.richTextBox2.Text = "select top 1000 * from pos_sales";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(20, 12);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(161, 12);
            this.label17.TabIndex = 46;
            this.label17.Text = "自写sql导出插入语句的脚本:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(536, 211);
            this.tabControl1.TabIndex = 53;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox2);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.textBox12);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.textBox11);
            this.tabPage1.Controls.Add(this.button22);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(528, 185);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "批量生成sql插入语句";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(528, 185);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "批量执行插入语句";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelCurrent);
            this.groupBox2.Controls.Add(this.textBoxConnectionString);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.numericUpDownTotalRecords);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.button13);
            this.groupBox2.Controls.Add(this.button12);
            this.groupBox2.Location = new System.Drawing.Point(17, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(494, 164);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "执行脚本设置";
            // 
            // labelCurrent
            // 
            this.labelCurrent.AutoSize = true;
            this.labelCurrent.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrent.Location = new System.Drawing.Point(119, 109);
            this.labelCurrent.Name = "labelCurrent";
            this.labelCurrent.Size = new System.Drawing.Size(19, 20);
            this.labelCurrent.TabIndex = 40;
            this.labelCurrent.Text = "0";
            // 
            // textBoxConnectionString
            // 
            this.textBoxConnectionString.Location = new System.Drawing.Point(66, 31);
            this.textBoxConnectionString.Multiline = true;
            this.textBoxConnectionString.Name = "textBoxConnectionString";
            this.textBoxConnectionString.Size = new System.Drawing.Size(410, 53);
            this.textBoxConnectionString.TabIndex = 39;
            this.textBoxConnectionString.Text = "Data Source=.\\sql2008;Initial Catalog=数据库名;Integrated Security=True";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 38;
            this.label12.Text = "连接参数:";
            // 
            // numericUpDownTotalRecords
            // 
            this.numericUpDownTotalRecords.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTotalRecords.Location = new System.Drawing.Point(8, 112);
            this.numericUpDownTotalRecords.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericUpDownTotalRecords.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTotalRecords.Name = "numericUpDownTotalRecords";
            this.numericUpDownTotalRecords.Size = new System.Drawing.Size(105, 21);
            this.numericUpDownTotalRecords.TabIndex = 36;
            this.numericUpDownTotalRecords.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 12);
            this.label11.TabIndex = 37;
            this.label11.Text = "每次执行sql语句条:";
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(258, 95);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(105, 50);
            this.button13.TabIndex = 27;
            this.button13.Text = "无日志清空数据";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(371, 95);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(105, 50);
            this.button12.TabIndex = 27;
            this.button12.Text = "执行sql脚本";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 211);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 55;
            this.label10.Text = "日志：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(4, 226);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(530, 201);
            this.richTextBox1.TabIndex = 54;
            this.richTextBox1.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(528, 185);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "批量执行sql脚本";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPs);
            this.groupBox1.Controls.Add(this.txtBatchSqlConnStr);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSelectAndExecSql);
            this.groupBox1.Location = new System.Drawing.Point(17, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 164);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "执行脚本设置";
            // 
            // txtBatchSqlConnStr
            // 
            this.txtBatchSqlConnStr.Location = new System.Drawing.Point(66, 31);
            this.txtBatchSqlConnStr.Multiline = true;
            this.txtBatchSqlConnStr.Name = "txtBatchSqlConnStr";
            this.txtBatchSqlConnStr.Size = new System.Drawing.Size(410, 53);
            this.txtBatchSqlConnStr.TabIndex = 39;
            this.txtBatchSqlConnStr.Text = "Data Source = 192.168.2.78;Initial Catalog = 数据库名;User Id = sa;Password =密码;";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "连接参数:";
            // 
            // btnSelectAndExecSql
            // 
            this.btnSelectAndExecSql.Location = new System.Drawing.Point(294, 95);
            this.btnSelectAndExecSql.Name = "btnSelectAndExecSql";
            this.btnSelectAndExecSql.Size = new System.Drawing.Size(182, 50);
            this.btnSelectAndExecSql.TabIndex = 27;
            this.btnSelectAndExecSql.Text = "批量选择sql脚本并且执行";
            this.btnSelectAndExecSql.UseVisualStyleBackColor = true;
            this.btnSelectAndExecSql.Click += new System.EventHandler(this.btnSelectAndExecSql_Click);
            // 
            // lblPs
            // 
            this.lblPs.AutoSize = true;
            this.lblPs.Location = new System.Drawing.Point(16, 133);
            this.lblPs.Name = "lblPs";
            this.lblPs.Size = new System.Drawing.Size(137, 12);
            this.lblPs.TabIndex = 40;
            this.lblPs.Text = "PS：单个文件一个个执行";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 433);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量生成和执行sql插入语句";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalRecords)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.TextBox textBoxConnectionString;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDownTotalRecords;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPs;
        private System.Windows.Forms.TextBox txtBatchSqlConnStr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectAndExecSql;
    }
}

