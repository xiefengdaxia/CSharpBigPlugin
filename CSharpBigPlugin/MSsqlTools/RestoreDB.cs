using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MSsqlTools
{
    public partial class RestoreDB : Form
    {
        public RestoreDB()
        {
            InitializeComponent();
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFromFile.Text = fileDialog.FileName;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
               txtToPath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (txtToPath.Text == string.Empty)
            {
                MessageBox.Show("请选择要恢复到的路径！");
                return;
            }
            if (txtFromFile.Text == string.Empty)
            {
                MessageBox.Show("请选择备份好的数据库文件！");
                return;
            }

            if (txtReName.Text.Contains("."))
            {
                MessageBox.Show("无需加后缀！");
                return;
            }
            try
            {
                var sql=string.Format("restore filelistonly from disk='{0}'",txtFromFile.Text);
                var list = DBHelper.QuerySql(sql);
                if (list.Count == 0)
                {
                    MessageBox.Show("错误，是否选择了不正确的备份文件？");
                    return;
                }
                var RestoreSql = string.Format(@"restore database [{2}] from disk='{0}' with move '{3}' to '{1}\{2}.MDF',move '{4}' to '{1}\{2}_LOG.LDF' ",txtFromFile.Text,txtToPath.Text,txtReName.Text,list[0],list[1]);
                DBHelper.execSql(RestoreSql);
                MessageBox.Show("恢复完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
            }
        }

        private void RestoreDB_Load(object sender, EventArgs e)
        {

        }
    }
}
