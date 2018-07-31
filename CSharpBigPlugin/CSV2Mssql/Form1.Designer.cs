namespace CSV2Mssql
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
            this.textBoxConnectionString = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnUpdateCardsno = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOldCardsno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewCardsno = new System.Windows.Forms.TextBox();
            this.txtM_id = new System.Windows.Forms.TextBox();
            this.btrCreateCSV = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSql = new System.Windows.Forms.TextBox();
            this.cbIgnoreFirstLine = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxConnectionString
            // 
            this.textBoxConnectionString.Location = new System.Drawing.Point(66, 27);
            this.textBoxConnectionString.Multiline = true;
            this.textBoxConnectionString.Name = "textBoxConnectionString";
            this.textBoxConnectionString.Size = new System.Drawing.Size(434, 53);
            this.textBoxConnectionString.TabIndex = 41;
            this.textBoxConnectionString.Text = "Data Source=.\\sql2008;Initial Catalog=数据库名;Integrated Security=True";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 40;
            this.label12.Text = "连接参数:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbIgnoreFirstLine);
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.lblStatus);
            this.groupBox2.Controls.Add(this.labelCurrent);
            this.groupBox2.Controls.Add(this.btnTestConn);
            this.groupBox2.Controls.Add(this.textBoxConnectionString);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(20, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(527, 246);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "导入到数据库";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(155, 134);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(161, 37);
            this.btnImport.TabIndex = 43;
            this.btnImport.Text = "选择文件*.csv并且导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.Location = new System.Drawing.Point(6, 206);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(296, 16);
            this.lblStatus.TabIndex = 48;
            this.lblStatus.Text = "导入状态:成功:0|失败:0|已存在:0|总:0";
            // 
            // labelCurrent
            // 
            this.labelCurrent.AutoSize = true;
            this.labelCurrent.Location = new System.Drawing.Point(6, 146);
            this.labelCurrent.Name = "labelCurrent";
            this.labelCurrent.Size = new System.Drawing.Size(83, 12);
            this.labelCurrent.TabIndex = 44;
            this.labelCurrent.Text = "csv记录统计:0";
            // 
            // btnTestConn
            // 
            this.btnTestConn.Location = new System.Drawing.Point(417, 86);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(83, 28);
            this.btnTestConn.TabIndex = 42;
            this.btnTestConn.Text = "测试连接";
            this.btnTestConn.UseVisualStyleBackColor = true;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnUpdateCardsno);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtOldCardsno);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtNewCardsno);
            this.groupBox3.Controls.Add(this.txtM_id);
            this.groupBox3.Location = new System.Drawing.Point(606, 160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(322, 211);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "物理卡号校准";
            // 
            // btnUpdateCardsno
            // 
            this.btnUpdateCardsno.Location = new System.Drawing.Point(224, 111);
            this.btnUpdateCardsno.Name = "btnUpdateCardsno";
            this.btnUpdateCardsno.Size = new System.Drawing.Size(75, 30);
            this.btnUpdateCardsno.TabIndex = 4;
            this.btnUpdateCardsno.Text = "修改";
            this.btnUpdateCardsno.UseVisualStyleBackColor = true;
            this.btnUpdateCardsno.Click += new System.EventHandler(this.btnUpdateCardsno_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(224, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 30);
            this.button3.TabIndex = 4;
            this.button3.Text = "查询";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "ps:卡号指的是ETS系统内会员卡卡号";
            // 
            // txtOldCardsno
            // 
            this.txtOldCardsno.Location = new System.Drawing.Point(113, 78);
            this.txtOldCardsno.Name = "txtOldCardsno";
            this.txtOldCardsno.Size = new System.Drawing.Size(94, 21);
            this.txtOldCardsno.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "原物理卡号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "新物理卡号:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "卡号:";
            // 
            // txtNewCardsno
            // 
            this.txtNewCardsno.Location = new System.Drawing.Point(113, 116);
            this.txtNewCardsno.Name = "txtNewCardsno";
            this.txtNewCardsno.Size = new System.Drawing.Size(94, 21);
            this.txtNewCardsno.TabIndex = 0;
            // 
            // txtM_id
            // 
            this.txtM_id.Location = new System.Drawing.Point(113, 35);
            this.txtM_id.Name = "txtM_id";
            this.txtM_id.Size = new System.Drawing.Size(94, 21);
            this.txtM_id.TabIndex = 0;
            // 
            // btrCreateCSV
            // 
            this.btrCreateCSV.Location = new System.Drawing.Point(779, 66);
            this.btrCreateCSV.Name = "btrCreateCSV";
            this.btrCreateCSV.Size = new System.Drawing.Size(98, 37);
            this.btrCreateCSV.TabIndex = 0;
            this.btrCreateCSV.Text = "生成csv文件";
            this.btrCreateCSV.UseVisualStyleBackColor = true;
            this.btrCreateCSV.Click += new System.EventHandler(this.btrCreateCSV_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblTotal);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtSql);
            this.groupBox4.Controls.Add(this.btrCreateCSV);
            this.groupBox4.Location = new System.Drawing.Point(17, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(911, 130);
            this.groupBox4.TabIndex = 49;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "新中新视图数据导出为csv文件";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(28, 78);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(89, 12);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "生成记录统计:0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Sql：";
            // 
            // txtSql
            // 
            this.txtSql.Location = new System.Drawing.Point(75, 22);
            this.txtSql.Multiline = true;
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(802, 27);
            this.txtSql.TabIndex = 1;
            this.txtSql.Text = "SELECT [工号],[姓名],[开卡日期],[有效期至],[账号],[卡号],[部门编码]  FROM tyg_accounts  WHERE [账号]>0 " +
    "--AND [开卡日期]>=\'2017-3-20\'";
            // 
            // cbIgnoreFirstLine
            // 
            this.cbIgnoreFirstLine.AutoSize = true;
            this.cbIgnoreFirstLine.Checked = true;
            this.cbIgnoreFirstLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreFirstLine.Location = new System.Drawing.Point(338, 146);
            this.cbIgnoreFirstLine.Name = "cbIgnoreFirstLine";
            this.cbIgnoreFirstLine.Size = new System.Drawing.Size(72, 16);
            this.cbIgnoreFirstLine.TabIndex = 49;
            this.cbIgnoreFirstLine.Text = "忽略首行";
            this.cbIgnoreFirstLine.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 432);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSV2SqlServerTools";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxConnectionString;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtM_id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewCardsno;
        private System.Windows.Forms.TextBox txtOldCardsno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btrCreateCSV;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtSql;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnUpdateCardsno;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.CheckBox cbIgnoreFirstLine;
    }
}

