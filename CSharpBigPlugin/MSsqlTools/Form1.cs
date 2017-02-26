using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace MSsqlTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string port = "0";

        //sql server 身份验证 连接字符串 
        string ConnstrSqlServer = "server=.\\xsql2008;uid=sa;pwd=1;database=master";
        //windows 身份验证连接字符串  
        string ConnstrWindows = "server=.\\xsql2008;database=master;Trusted_Connection=SSPI";

        System.Timers.Timer timer;
        private void Form1_Load(object sender, EventArgs e)
        {
           timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
           

        }

        private void NewMethod(string ConnstrWindows)
        {
            SqlConnection conn = new SqlConnection(ConnstrWindows);
            try
            {
                conn.Open();
                string sql = "SELECT *  FROM sys.dm_exec_connections WHERE session_id=@@SPID ";
                //var sql = "select getdate()";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //port = dr["local_tcp_port"].ToString();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        ApendLog("field:" + dr.GetName(i) + " value:" + dr[i].ToString());
                    }
                }

                MessageBox.Show(port.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
      public static ServiceController server;
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //获得服务集合
            var serviceControllers = ServiceController.GetServices();
            //lambda查询服务名和服务状态
            server = serviceControllers.FirstOrDefault(service => service.ServiceName == "MSSQL$XSQL2008");
            //server = new ServiceController("MSSQL$XSQL20081");
            if (server == null)
            {
                UpdateTextBoxText(txtStatus,"未安装...");

                foreach (Control item in this.Controls)
                {
                    if (item is Button)
                    {
                       Button btn= ((Button)item);
                        if (btn.Name != "btnInstall")
                        {
                            btn.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                btnInstall.Enabled = false;
                if (server.Status != ServiceControllerStatus.Running)
                {
                    UpdateTextBoxText(txtStatus,"已停止...");
                    UpdateStatus(btnStart, true);
                    UpdateStatus(btnStop, false);
                }
                else
                {
                    UpdateTextBoxText(txtStatus,"运行中...");
                    UpdateStatus(btnStart, false);
                    UpdateStatus(btnStop, true);
                }
                if (port != "0")
                    return;
                RegistryKey Key;
                RegistryKey software;
                Key = Registry.LocalMachine;
                try
                {
                    software = Key.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSSQL10_50.XSQL2008\\MSSQLServer\\SuperSocketNetLib\\Tcp\\IPAll\\", true);
                    port = software.GetValue("TcpPort").ToString();
                    //如果ipall的端口是空，则是使用了动态端口
                    if (port == string.Empty)
                    {
                        RegistryKey DynamicPort = Key.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSSQL10_50.XSQL2008\\MSSQLServer\\SuperSocketNetLib\\Tcp\\IPAll\\", true);
                        port = software.GetValue("TcpDynamicPorts").ToString();

                    }
                    software.Close();
                }
                catch (Exception ex)
                {
                    ApendLog("获取端口报错:" + ex.Message + "\n\r" + ex.StackTrace);
                    port = "-1";
                }
                finally
                {
                    //txtRemoteConn.Text = "127.0.0.1," + port;
                    UpdateTextBoxText(txtRemoteConn, "127.0.0.1," + port);
                }
            }
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="text">日志text</param>
        public void ApendLog(string text)
        {
            this.Invoke(new Func<bool>(delegate()
            {
                richTextBox1.AppendText(DateTime.Now.ToString() + ":" + text + "\n\r");
                richTextBox1.ScrollToCaret();
                return true;
            }));
        }
        /// <summary>
        /// 修改文本框的内容
        /// </summary>
        /// <param name="text">内容</param>
        public void UpdateTextBoxText(TextBox txtbox,string text)
        {
            this.Invoke(new Func<bool>(delegate()
            {
                if (!txtbox.IsDisposed)
                {
                    txtbox.Text = text;
                }
                return true;
            }));
        }
        /// <summary>
        /// 修改启用和停止服务按钮状态
        /// </summary>
        /// <param name="status">状态</param>
        public void UpdateStatus(Button btn, bool status)
        {
            this.Invoke(new Func<bool>(delegate()
            {
                if (!btn.IsDisposed)
                {
                    btn.Enabled = status;
                }
                return true;
            }));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (server.Status == ServiceControllerStatus.Stopped)
            {
                server.Start();
                //server.WaitForStatus(ServiceControllerStatus.Running);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
        }

        private void btnUpdatePort_Click(object sender, EventArgs e)
        {
            UpdatePort form = new UpdatePort();
            form.ShowDialog();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.Status == ServiceControllerStatus.Running)
            {
                server.Stop();
                //server.WaitForStatus(ServiceControllerStatus.Stopped);
            }
        }
        
    }
}
