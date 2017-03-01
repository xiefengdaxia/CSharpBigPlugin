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
    public partial class AttachDB : Form
    {
        public AttachDB()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpenFileDialogForMdf_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog(); //new一个方法
            ofd.Filter = "*.mdf|*.mdf*"; //删选、设定文件显示类型
            ofd.ShowDialog(); //显示打开文件的窗口
           txtMDFPath.Text = ofd.FileName; //获得选择的文件路径
           txtRename.Text = ofd.SafeFileName.Split('.')[0];
        }

        private void btnOpenFileDialogForLdf_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog(); //new一个方法
            ofd.Filter = "*.ldf|*.ldf*"; //删选、设定文件显示类型
            ofd.ShowDialog(); //显示打开文件的窗口
            txtLDFPath.Text = ofd.FileName; //获得选择的文件路径
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            try
            {
               var result= DBHelper.execSql(string.Format("EXEC sp_attach_db @dbname = '{0}', @filename1 = '{1}',@filename2= '{2}'  ",txtRename.Text.Trim(),txtMDFPath.Text.Trim(),txtLDFPath.Text.Trim()));
               if (result == -1)
               {
                   MessageBox.Show("附加成功！");
               }
               else
               {
                   MessageBox.Show("附加失败！");
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
            }
        }
    }
}
