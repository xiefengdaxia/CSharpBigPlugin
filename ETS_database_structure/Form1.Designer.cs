namespace ETS_database_structure
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvTableAttribute = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBuildDoc = new System.Windows.Forms.Button();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveChange = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rtnCurrentTable = new System.Windows.Forms.RadioButton();
            this.rbtAllTable = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkXML = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.chkIgnoreNull = new System.Windows.Forms.CheckBox();
            this.btnsearchField = new System.Windows.Forms.Button();
            this.txtField = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkAccurate = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableAttribute)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(313, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(88, 33);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询 F5";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvTableAttribute
            // 
            this.dgvTableAttribute.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTableAttribute.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvTableAttribute.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvTableAttribute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableAttribute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13});
            this.dgvTableAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableAttribute.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvTableAttribute.Location = new System.Drawing.Point(0, 0);
            this.dgvTableAttribute.Name = "dgvTableAttribute";
            this.dgvTableAttribute.RowTemplate.Height = 23;
            this.dgvTableAttribute.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTableAttribute.Size = new System.Drawing.Size(1104, 438);
            this.dgvTableAttribute.TabIndex = 1;
            this.dgvTableAttribute.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgvTableAttribute.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "表名";
            this.Column1.HeaderText = "表名";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "字段序号";
            this.Column2.HeaderText = "序号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "字段名";
            this.Column3.HeaderText = "字段名";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "标识";
            this.Column4.HeaderText = "标识列";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "主键";
            this.Column5.HeaderText = "主键";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "类型";
            this.Column6.HeaderText = "类型";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "占用字节数";
            this.Column7.HeaderText = "占用字节";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "长度";
            this.Column8.HeaderText = "长度";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "小数位数";
            this.Column9.HeaderText = "小数位数";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "允许空";
            this.Column10.HeaderText = "允许空";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "默认值";
            this.Column11.HeaderText = "默认值";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column12.DataPropertyName = "备注";
            this.Column12.HeaderText = "字段备注";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 78;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "重新备注";
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Column13.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column13.HeaderText = "修改备注";
            this.Column13.MinimumWidth = 100;
            this.Column13.Name = "Column13";
            // 
            // btnBuildDoc
            // 
            this.btnBuildDoc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBuildDoc.Location = new System.Drawing.Point(960, 6);
            this.btnBuildDoc.Name = "btnBuildDoc";
            this.btnBuildDoc.Size = new System.Drawing.Size(101, 48);
            this.btnBuildDoc.TabIndex = 0;
            this.btnBuildDoc.Text = "生成MarkdownOrXML\r\nCtrl+P";
            this.btnBuildDoc.UseVisualStyleBackColor = true;
            this.btnBuildDoc.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtTableName
            // 
            this.txtTableName.Font = new System.Drawing.Font("宋体", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTableName.Location = new System.Drawing.Point(73, 11);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(180, 36);
            this.txtTableName.TabIndex = 2;
            this.txtTableName.Text = "mem_member";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "表名：";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(771, 13);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(180, 36);
            this.textBox2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(422, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "当前选中：";
            // 
            // btnSaveChange
            // 
            this.btnSaveChange.Location = new System.Drawing.Point(987, 12);
            this.btnSaveChange.Name = "btnSaveChange";
            this.btnSaveChange.Size = new System.Drawing.Size(78, 36);
            this.btnSaveChange.TabIndex = 4;
            this.btnSaveChange.Text = "保存更改Ctrl+S";
            this.btnSaveChange.UseVisualStyleBackColor = true;
            this.btnSaveChange.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(667, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "表名备注：";
            // 
            // rtnCurrentTable
            // 
            this.rtnCurrentTable.AutoSize = true;
            this.rtnCurrentTable.Checked = true;
            this.rtnCurrentTable.Location = new System.Drawing.Point(797, 11);
            this.rtnCurrentTable.Name = "rtnCurrentTable";
            this.rtnCurrentTable.Size = new System.Drawing.Size(83, 16);
            this.rtnCurrentTable.TabIndex = 5;
            this.rtnCurrentTable.TabStop = true;
            this.rtnCurrentTable.Text = "当前选中表";
            this.rtnCurrentTable.UseVisualStyleBackColor = true;
            // 
            // rbtAllTable
            // 
            this.rbtAllTable.AutoSize = true;
            this.rbtAllTable.Location = new System.Drawing.Point(898, 11);
            this.rbtAllTable.Name = "rbtAllTable";
            this.rbtAllTable.Size = new System.Drawing.Size(47, 16);
            this.rbtAllTable.TabIndex = 5;
            this.rbtAllTable.Text = "所有";
            this.rbtAllTable.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1104, 558);
            this.panel1.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvTableAttribute);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 58);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1104, 438);
            this.panel4.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkXML);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.textBox4);
            this.panel3.Controls.Add(this.chkIgnoreNull);
            this.panel3.Controls.Add(this.btnsearchField);
            this.panel3.Controls.Add(this.btnBuildDoc);
            this.panel3.Controls.Add(this.rbtAllTable);
            this.panel3.Controls.Add(this.rtnCurrentTable);
            this.panel3.Controls.Add(this.txtField);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 496);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1104, 62);
            this.panel3.TabIndex = 0;
            // 
            // chkXML
            // 
            this.chkXML.AutoSize = true;
            this.chkXML.Location = new System.Drawing.Point(917, 33);
            this.chkXML.Name = "chkXML";
            this.chkXML.Size = new System.Drawing.Size(42, 16);
            this.chkXML.TabIndex = 8;
            this.chkXML.Text = "xml";
            this.chkXML.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1062, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 39);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(342, 11);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(449, 39);
            this.textBox4.TabIndex = 7;
            this.textBox4.Text = resources.GetString("textBox4.Text");
            // 
            // chkIgnoreNull
            // 
            this.chkIgnoreNull.AutoSize = true;
            this.chkIgnoreNull.Location = new System.Drawing.Point(797, 34);
            this.chkIgnoreNull.Name = "chkIgnoreNull";
            this.chkIgnoreNull.Size = new System.Drawing.Size(120, 16);
            this.chkIgnoreNull.TabIndex = 6;
            this.chkIgnoreNull.Text = "忽略空备注的字段";
            this.chkIgnoreNull.UseVisualStyleBackColor = true;
            // 
            // btnsearchField
            // 
            this.btnsearchField.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnsearchField.Location = new System.Drawing.Point(209, 11);
            this.btnsearchField.Name = "btnsearchField";
            this.btnsearchField.Size = new System.Drawing.Size(86, 39);
            this.btnsearchField.TabIndex = 0;
            this.btnsearchField.Text = "检索字段名Ctrl+F";
            this.btnsearchField.UseVisualStyleBackColor = true;
            this.btnsearchField.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtField
            // 
            this.txtField.Font = new System.Drawing.Font("宋体", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtField.Location = new System.Drawing.Point(8, 11);
            this.txtField.Name = "txtField";
            this.txtField.Size = new System.Drawing.Size(180, 36);
            this.txtField.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkAccurate);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnSaveChange);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.txtTableName);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1104, 58);
            this.panel2.TabIndex = 0;
            // 
            // chkAccurate
            // 
            this.chkAccurate.AutoSize = true;
            this.chkAccurate.Checked = true;
            this.chkAccurate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAccurate.Location = new System.Drawing.Point(259, 23);
            this.chkAccurate.Name = "chkAccurate";
            this.chkAccurate.Size = new System.Drawing.Size(48, 16);
            this.chkAccurate.TabIndex = 6;
            this.chkAccurate.Text = "精确";
            this.chkAccurate.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(511, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "NULL";
            // 
            // Form1
            // 
            this.AcceptButton = this.btnsearchField;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 558);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ETS数据库字典生成器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableAttribute)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvTableAttribute;
        private System.Windows.Forms.Button btnBuildDoc;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveChange;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rtnCurrentTable;
        private System.Windows.Forms.RadioButton rbtAllTable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkAccurate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.Button btnsearchField;
        private System.Windows.Forms.TextBox txtField;
        private System.Windows.Forms.CheckBox chkIgnoreNull;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chkXML;
    }
}

