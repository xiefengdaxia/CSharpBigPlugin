using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace MSsqlTools
{
    public partial class UpdatePort : Form
    {
        public UpdatePort()
        {
            InitializeComponent();
        }

        private void UpdatePort_Load(object sender, EventArgs e)
        {
            txtPort.Text = MainForm.port;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdatePort_Click(object sender, EventArgs e)
        {
            UpdatePortInRegedit("SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSSQL10_50.XSQL2008\\MSSQLServer\\SuperSocketNetLib\\Tcp\\IPAll\\", "TcpPort", txtPort.Text.Trim());
            UpdatePortInRegedit("SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSSQL10_50.XSQL2008\\MSSQLServer\\SuperSocketNetLib\\Tcp\\", "TcpPort", txtPort.Text.Trim());
            UpdatePortInRegedit("SOFTWARE\\WOW6432Node\\Microsoft\\Microsoft SQL Server\\XSQL2008\\MSSQLServer\\SuperSocketNetLib\\Tcp\\", "TcpPort", txtPort.Text.Trim());
            UpdatePortInRegedit("SOFTWARE\\WOW6432Node\\Microsoft\\Microsoft SQL Server\\MSSQL10_50.XSQL2008\\MSSQLServer\\SuperSocketNetLib\\Tcp\\IPAll\\", "TcpDynamicPorts", txtPort.Text.Trim());
            UpdatePortInRegedit("SOFTWARE\\WOW6432Node\\Microsoft\\Microsoft SQL Server\\MSSQL10_50.XSQL2008\\MSSQLServer\\SuperSocketNetLib\\Tcp\\IPAll\\", "TcpPort", txtPort.Text.Trim());
            MessageBox.Show("修改端口重启才可生效！正在重启中...");

            ServiceController service = MainForm.server;
            try
            {
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
            }

            Application.Restart();
        }

        /// <summary>
        /// 修改注册表
        /// </summary>
        /// <param name="key">注册表键</param>
        /// <param name="name">项</param>
        /// <param name="value">值</param>
        private void UpdatePortInRegedit(string key, string name, string value)
        {
            RegistryKey RootKey;
            RegistryKey SubKey;
            RootKey = Registry.LocalMachine;
            try
            {
                SubKey = RootKey.OpenSubKey(key, true);
                if (SubKey != null)
                {
                    SubKey.SetValue(name, value);
                }
                SubKey.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
            }
        }
    }
}
