namespace ShrinkDatabase
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
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrentTableName = new System.Windows.Forms.TextBox();
            this.tableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.records = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reservedSpace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.useSpace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indexUseSpace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unuseSpace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTable
            // 
            this.dgvTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tableName,
            this.records,
            this.reservedSpace,
            this.useSpace,
            this.indexUseSpace,
            this.unuseSpace});
            this.dgvTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvTable.Location = new System.Drawing.Point(2, 1);
            this.dgvTable.MultiSelect = false;
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.ReadOnly = true;
            this.dgvTable.RowTemplate.Height = 23;
            this.dgvTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTable.Size = new System.Drawing.Size(679, 248);
            this.dgvTable.TabIndex = 2;
            this.dgvTable.SelectionChanged += new System.EventHandler(this.dgvTable_SelectionChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 46);
            this.button1.TabIndex = 3;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1, 376);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(679, 127);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 361);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "日志";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(329, 308);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 46);
            this.button2.TabIndex = 3;
            this.button2.Text = "重建索引";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(621, 308);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 39);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(461, 308);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 46);
            this.button3.TabIndex = 3;
            this.button3.Text = "数据库收缩";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(557, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "ps:未用空间大的一般是碎片居多，重建索引可清理碎片，减少未用空间，此时收缩数据库会见效明显。\r\n另外有的数据库表没有聚集索引，需要自己加，然后删除，一样可以达到" +
    "清理碎片减少未用空间的目的。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "当前选中:";
            // 
            // CurrentTableName
            // 
            this.CurrentTableName.Location = new System.Drawing.Point(202, 323);
            this.CurrentTableName.Name = "CurrentTableName";
            this.CurrentTableName.Size = new System.Drawing.Size(100, 21);
            this.CurrentTableName.TabIndex = 9;
            // 
            // tableName
            // 
            this.tableName.DataPropertyName = "表名";
            this.tableName.HeaderText = "表名";
            this.tableName.Name = "tableName";
            this.tableName.ReadOnly = true;
            // 
            // records
            // 
            this.records.DataPropertyName = "记录数";
            this.records.HeaderText = "记录数";
            this.records.Name = "records";
            this.records.ReadOnly = true;
            // 
            // reservedSpace
            // 
            this.reservedSpace.DataPropertyName = "预留空间";
            this.reservedSpace.HeaderText = "预留空间";
            this.reservedSpace.Name = "reservedSpace";
            this.reservedSpace.ReadOnly = true;
            // 
            // useSpace
            // 
            this.useSpace.DataPropertyName = "使用空间";
            this.useSpace.HeaderText = "使用空间";
            this.useSpace.Name = "useSpace";
            this.useSpace.ReadOnly = true;
            // 
            // indexUseSpace
            // 
            this.indexUseSpace.DataPropertyName = "索引占用空间";
            this.indexUseSpace.HeaderText = "索引占用空间";
            this.indexUseSpace.Name = "indexUseSpace";
            this.indexUseSpace.ReadOnly = true;
            // 
            // unuseSpace
            // 
            this.unuseSpace.DataPropertyName = "未用空间";
            this.unuseSpace.HeaderText = "未用空间";
            this.unuseSpace.Name = "unuseSpace";
            this.unuseSpace.ReadOnly = true;
            // 
            // Form1
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(683, 504);
            this.Controls.Add(this.CurrentTableName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库收缩工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CurrentTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn records;
        private System.Windows.Forms.DataGridViewTextBoxColumn reservedSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn useSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexUseSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn unuseSpace;
    }
}

