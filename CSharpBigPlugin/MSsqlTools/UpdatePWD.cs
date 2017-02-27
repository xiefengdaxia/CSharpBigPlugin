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
    public partial class UpdatePWD : Form
    {
        public UpdatePWD()
        {
            InitializeComponent();
        }

        private void btnUpdatePWD_Click(object sender, EventArgs e)
        {
            try
            {
                var result = DBHelper.execSql("EXECUTE sp_password NULL,'" + txtPWD.Text + "','sa'");
                if ( result==-1)
                {
                    MessageBox.Show("修改成功！");
                }
                else
                {
                    MessageBox.Show("修改失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
