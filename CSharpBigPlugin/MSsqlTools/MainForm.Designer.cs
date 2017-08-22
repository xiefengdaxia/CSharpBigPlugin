namespace MSsqlTools
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnUpdatePort = new System.Windows.Forms.Button();
            this.btnUpdatePWD = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestoreDB = new System.Windows.Forms.Button();
            this.btnAttachDB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnOpenIsqlw = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.txtRemoteConn = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.插件设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.捐赠ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版权申明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(442, 32);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.Text = "一键安装";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnUninstall
            // 
            this.btnUninstall.Location = new System.Drawing.Point(442, 61);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(75, 23);
            this.btnUninstall.TabIndex = 0;
            this.btnUninstall.Text = "一键卸载";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(442, 90);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "启动服务";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(442, 119);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "停止服务";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnUpdatePort
            // 
            this.btnUpdatePort.Location = new System.Drawing.Point(442, 148);
            this.btnUpdatePort.Name = "btnUpdatePort";
            this.btnUpdatePort.Size = new System.Drawing.Size(75, 23);
            this.btnUpdatePort.TabIndex = 0;
            this.btnUpdatePort.Text = "修改端口";
            this.btnUpdatePort.UseVisualStyleBackColor = true;
            this.btnUpdatePort.Click += new System.EventHandler(this.btnUpdatePort_Click);
            // 
            // btnUpdatePWD
            // 
            this.btnUpdatePWD.Location = new System.Drawing.Point(442, 177);
            this.btnUpdatePWD.Name = "btnUpdatePWD";
            this.btnUpdatePWD.Size = new System.Drawing.Size(75, 23);
            this.btnUpdatePWD.TabIndex = 0;
            this.btnUpdatePWD.Text = "修改sa密码";
            this.btnUpdatePWD.UseVisualStyleBackColor = true;
            this.btnUpdatePWD.Click += new System.EventHandler(this.btnUpdatePWD_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(442, 206);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(75, 23);
            this.btnBackup.TabIndex = 0;
            this.btnBackup.Text = "备份数据库";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnRestoreDB
            // 
            this.btnRestoreDB.Location = new System.Drawing.Point(442, 235);
            this.btnRestoreDB.Name = "btnRestoreDB";
            this.btnRestoreDB.Size = new System.Drawing.Size(75, 23);
            this.btnRestoreDB.TabIndex = 0;
            this.btnRestoreDB.Text = "还原数据库";
            this.btnRestoreDB.UseVisualStyleBackColor = true;
            this.btnRestoreDB.Click += new System.EventHandler(this.btnRestoreDB_Click);
            // 
            // btnAttachDB
            // 
            this.btnAttachDB.Location = new System.Drawing.Point(442, 264);
            this.btnAttachDB.Name = "btnAttachDB";
            this.btnAttachDB.Size = new System.Drawing.Size(75, 23);
            this.btnAttachDB.TabIndex = 0;
            this.btnAttachDB.Text = "附加数据库";
            this.btnAttachDB.UseVisualStyleBackColor = true;
            this.btnAttachDB.Click += new System.EventHandler(this.btnAttachDB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "实例名:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(76, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "xsql2008";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "状  态:";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(76, 69);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(100, 21);
            this.txtStatus.TabIndex = 2;
            this.txtStatus.Text = "查询中...";
            // 
            // btnOpenIsqlw
            // 
            this.btnOpenIsqlw.Location = new System.Drawing.Point(443, 293);
            this.btnOpenIsqlw.Name = "btnOpenIsqlw";
            this.btnOpenIsqlw.Size = new System.Drawing.Size(75, 23);
            this.btnOpenIsqlw.TabIndex = 0;
            this.btnOpenIsqlw.Text = "查询分析器";
            this.btnOpenIsqlw.UseVisualStyleBackColor = true;
            this.btnOpenIsqlw.Click += new System.EventHandler(this.btnOpenIsqlw_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "本地连接:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(211, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "远程连接:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(276, 39);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(127, 21);
            this.textBox3.TabIndex = 2;
            this.textBox3.Text = ".\\xsql2008";
            // 
            // txtRemoteConn
            // 
            this.txtRemoteConn.Location = new System.Drawing.Point(276, 69);
            this.txtRemoteConn.Name = "txtRemoteConn";
            this.txtRemoteConn.ReadOnly = true;
            this.txtRemoteConn.Size = new System.Drawing.Size(127, 21);
            this.txtRemoteConn.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(25, 119);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(378, 177);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "日志:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(166, 307);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(239, 12);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Microsoft SQL Management Studio Express";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 307);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "如需更丰富的体验请下载";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插件设置ToolStripMenuItem,
            this.插件ToolStripMenuItem,
            this.关于ToolStripMenuItem,
            this.捐赠ToolStripMenuItem,
            this.版权申明ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(529, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 插件设置ToolStripMenuItem
            // 
            this.插件设置ToolStripMenuItem.Name = "插件设置ToolStripMenuItem";
            this.插件设置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.插件设置ToolStripMenuItem.Text = "插件设置";
            this.插件设置ToolStripMenuItem.Click += new System.EventHandler(this.插件设置ToolStripMenuItem_Click);
            // 
            // 插件ToolStripMenuItem
            // 
            this.插件ToolStripMenuItem.Name = "插件ToolStripMenuItem";
            this.插件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.插件ToolStripMenuItem.Text = "插件";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // 捐赠ToolStripMenuItem
            // 
            this.捐赠ToolStripMenuItem.Name = "捐赠ToolStripMenuItem";
            this.捐赠ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.捐赠ToolStripMenuItem.Text = "捐赠";
            this.捐赠ToolStripMenuItem.Click += new System.EventHandler(this.捐赠ToolStripMenuItem_Click);
            // 
            // 版权申明ToolStripMenuItem
            // 
            this.版权申明ToolStripMenuItem.Name = "版权申明ToolStripMenuItem";
            this.版权申明ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.版权申明ToolStripMenuItem.Text = "版权申明";
            this.版权申明ToolStripMenuItem.Click += new System.EventHandler(this.版权申明ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(529, 332);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.txtRemoteConn);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenIsqlw);
            this.Controls.Add(this.btnAttachDB);
            this.Controls.Add(this.btnRestoreDB);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnUpdatePWD);
            this.Controls.Add(this.btnUpdatePort);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnUninstall);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SqlServer 2008 R2 控制台 1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnUpdatePort;
        private System.Windows.Forms.Button btnUpdatePWD;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnRestoreDB;
        private System.Windows.Forms.Button btnAttachDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnOpenIsqlw;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox txtRemoteConn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 插件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 捐赠ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版权申明ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插件设置ToolStripMenuItem;
    }
}

