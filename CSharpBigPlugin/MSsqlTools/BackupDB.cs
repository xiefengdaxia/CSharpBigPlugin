using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MSsqlTools
{
    public partial class BackupDB : Form
    {
        public BackupDB()
        {
            InitializeComponent();
        }

        private void BackupDB_Load(object sender, EventArgs e)
        {
            var list = DBHelper.QuerySql("select [name] from [sysdatabases] order by [name]");
            foreach (var item in list)
            {
                cbDBNames.Items.Add(item);
            }
            cbDBNames.Text = cbDBNames.Items[0].ToString();
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (cbDBNames.Text == string.Empty)
            {
                MessageBox.Show("请选择要备份的数据库！");
                return;
            }
            if (txtPath.Text == string.Empty)
            {
                MessageBox.Show("请选择备份数据库需要存放的路径！");
                return;
            }


            try
            {
                DBHelper.execSql(string.Format(@"backup database {0} to disk='{1}\{0}-{2}.bak'  with init,name='{0}-{2}.bak'  --完全备份 ", cbDBNames.Text, txtPath.Text, DateTime.Now.ToString("yyyy-MM-dd")));
                MessageBox.Show("备份完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
            }

        }
    }
}
