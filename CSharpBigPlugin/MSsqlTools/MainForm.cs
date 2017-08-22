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
using System.IO;
using System.Threading;
using System.Reflection;
using CSPluginKernel;
using System.Collections;
using System.Runtime.Remoting;

namespace MSsqlTools
{
    public partial class MainForm : Form,CSPluginKernel.IApplicationObject
    {
        public MainForm()
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

            try
            {
                //MessageBox.Show("1");
                this.LoadAllPlugins();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
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
                UpdateStatus(btnInstall, false);
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
            ApendLog("启动SqlServer服务");
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
            ApendLog("停止SqlServer服务");
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            //后台创建线程避免耗时
            Thread thread = new Thread(delegate()
                {
                    CreateRegFiles();
                }
            );
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            var cmdcode = @"
                reg import """ + path + @"\install.reg""
                c:\Windows\SysWOW64\reg.exe import """ + path + @"\install.reg""
                sc create ""MSSQL$XSQL2008"" binpath= """ + path + @"\MSSQL10_50.XSQL2008\MSSQL\Binn\sqlservr.exe -sXSQL2008"" start= auto displayname= ""SQL Server (XSQL2008)""
                sc description ""MSSQL$XSQL2008"" ""提供数据的存储、处理和受控访问，并提供快速的事务处理。""
                sc config ""MSSQL$XSQL2008"" start= auto
                net start ""MSSQL$XSQL2008""
                sc start ""MSSQL$XSQL2008""";
            RunCmdCode(cmdcode);
            ApendLog("安装完成！");
            Application.Restart();
        }

        #region 根据路径创建Reg文件
        /// <summary>
        /// 根据路径创建Reg文件
        /// </summary>
        string path;
        private  void CreateRegFiles()
        {
             path = @System.Environment.CurrentDirectory.Replace("\\", "\\\\");
            StringBuilder sb = new StringBuilder();
            sb.Append(@"Windows Registry Editor Version 5.00

[HKEY_LOCAL_MACHINE\SOFTWARE]

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft]

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server]

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\ExceptionMessageBox]
""ShowFeedbackButton""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names]

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL]
""XSQL2008""=""MSSQL10_50.XSQL2008""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\XSQL2008]

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\XSQL2008\MSSQLServer]

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\XSQL2008\MSSQLServer\CurrentVersion]
""CurrentVersion""=""10.50.6000.34""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\XSQL2008\MSSQLServer\SuperSocketNetLib]
""ProtocolList""=hex(7):74,00,63,00,70,00,00,00,6e,00,70,00,00,00,00,00

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\XSQL2008\MSSQLServer\SuperSocketNetLib\Np]
""PipeName""=""\\\\.\\pipe\\MSSQL$XSQL2008\\sql\\query""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\XSQL2008\MSSQLServer\SuperSocketNetLib\Tcp]
""TcpPort""=""49157""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\XSQL2008\Setup]
""SQLPath""=""" + path + @"\\MSSQL10_50.XSQL2008\\MSSQL""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008]
@=""XSQL2008""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\ClusterState]
""MPT_AGENT_CORE_CNI""=dword:00000000
""SQL_Engine_Core_Inst""=dword:00000000

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\ConfigurationState]
""MPT_AGENT_CORE_CNI""=dword:00000001
""SQL_Engine_Core_Inst""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\CPE]
""CollectorParameters""=""SQL MSSQL10_50.XSQL2008""
""TimeOfReporting""=dword:0000003c
""ErrorDumpDir""=""" + path + @"\\MSSQL10_50.XSQL2008\\MSSQL\\LOG\\""
""EnableErrorReporting""=dword:00000000
""CustomerFeedback""=dword:00000000

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer]
""SysDb""=dword:00000001
""AuditLevel""=dword:00000002
""BackupDirectory""=""" + path + @"\\MSSQL10_50.XSQL2008\\MSSQL\\Backup""
""DefaultLogin""=""guest""
""LoginMode""=dword:00000002
""Map#""=""-""
""Map$""=""""
""Map_""=""[DomainSeperator]""
""Tapeloadwaittime""=dword:ffffffff
""SetHostName""=dword:00000000
""FirstStart""=dword:00000000
""uptime_pid""=dword:00000918
""uptime_time_utc""=hex:bc,3e,8b,27,b3,d1,d1,01

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer\CurrentVersion]
""RegisteredOwner""=""""
""SerialNumber""=""""
""CurrentVersion""=""10.50.6000.34""
""Language""=dword:00000804

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer\Filestream]
""RsFxVersion""=""0153""
""RsFxVersionBase""=""0150""
""RsFxVersionPcu""=""0153""
""InstanceGuid""=""{b7039053-95ae-4086-9367-3805aae5ab05}""
""ShareName""=""XSQL2008""
""EnableLevel""=dword:00000000

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer\Parameters]
""SQLArg0""=""-d" + path + @"\\MSSQL10_50.XSQL2008\\MSSQL\\DATA\\master.mdf""
""SQLArg1""=""-e" + path + @"\\MSSQL10_50.XSQL2008\\MSSQL\\Log\\ERRORLOG""
""SQLArg2""=""-l" + path + @"\\MSSQL10_50.XSQL2008\\MSSQL\\DATA\\mastlog.ldf""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer\SuperSocketNetLib]
""ForceEncryption""=dword:00000000
""HideInstance""=dword:00000000
""Certificate""=""""
""ExtendedProtection""=dword:00000000
""AcceptedSPNs""=hex(7):00,00
""DisplayName""=""SQL Server Network Configuration""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer\SuperSocketNetLib\Np]
""Enabled""=dword:00000000
""PipeName""=""\\\\.\\pipe\\MSSQL$XSQL2008\\sql\\query""
""DisplayName""=""Named Pipes""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer\SuperSocketNetLib\Sm]
""Enabled""=dword:00000001
""DisplayName""=""Shared Memory""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer\SuperSocketNetLib\Tcp]
""Enabled""=dword:00000001
""ListenOnAllIPs""=dword:00000001
""KeepAlive""=dword:00007530
""DisplayName""=""TCP/IP""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer\SuperSocketNetLib\Tcp\IPAll]
""TcpPort""=""""
""TcpDynamicPorts""=""59157""
""DisplayName""=""Any IP Address""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer\SuperSocketNetLib\Tcp\IP1]
""Enabled""=dword:00000000
""Active""=dword:00000001
""TcpPort""=""""
""TcpDynamicPorts""=""0""
""DisplayName""=""Specific IP Address""
""IpAddress""=""127.0.0.1""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer\SuperSocketNetLib\Tcp\IP2]
""Enabled""=dword:00000000
""Active""=dword:00000001
""TcpPort""=""""
""TcpDynamicPorts""=""0""
""DisplayName""=""Specific IP Address""
""IpAddress""=""::1""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\MSSQLServer\SuperSocketNetLib\Via]
""Enabled""=dword:00000000
""DefaultServerPort""=""0:1433""
""ListenInfo""=""0:1433""
""DisplayName""=""VIA""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Providers]
""AllowInProcess""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Providers\ADSDSOObject]
""AllowInProcess""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Providers\DB2OLEDB]
""AllowInProcess""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Providers\Microsoft.Jet.OLEDB.4.0]
""AllowInProcess""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Providers\MSDAORA]
""AllowInProcess""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Providers\MSDASQL]
""AllowInProcess""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Providers\MSIDXS]
""AllowInProcess""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Providers\MSOLAP]
""AllowInProcess""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Providers\MSQLImpProv]
""AllowInProcess""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Providers\MSSEARCHSQL]
""AllowInProcess""=dword:00000001

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Providers\SQLNCLI10]
""AllowInProcess""=dword:00000001
""DisallowAdhocAccess""=dword:00000000

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Security]
""Entropy""=hex:87,eb,4f,14,4d,24,b1,f6,af,df,92,ec,ca,dc,f1,dd,3d,59,9e,93,63,\
  12,c1,eb,46,7d,80,5d,60,ca,39,4b,e6,00,bb,33,1f,7a,d0,c7,12,85,f8,68,35,79,\
  14,c7,7b,85,7b,00,d5,55,87,a8,c0,b7,e8,97,dd,ef,c6,fa,26,ec,7f,33,e8,66,fb,\
  13,bd,52,8e,cf,ef,f1,b0,da,5f,ee,4c,d0,05,b3,ec,41,dc,70,d3,64,ca,db,06,1f,\
  d8,da,cc,38,7d,12,3e,b7,fa,56,9d,db,58,13,2a,9c,bc,0a,2c,21,47,66,c6,67,c9,\
  26,e8,e0,e8,26,86,6e,bc,8a,68,c5,d1,8e,28,7c,23,7d,4c,77,1e,96,22,7a,00,6a,\
  a0,cd,a8,3f,ed,5b,70,e0,41,24,7f,ec,e7,e6,51,fc,57,d5,ac,57,a8,6e,3e,c1,e0,\
  68,bb,7e,cc,7f,86,18,d1,60,57,9e,cc,f4,4e,af,94,8e,e6,5a,ed,9a,8e,99,60,0b,\
  9a,8a,b2,f4,7d,b8,0a,63,51,8e,7e,41,26,0c,b0,59,21,f0,7d,a2,2c,02,51,17,53,\
  8d,fc,fb,19,c0,fd,bf,3d,e6,3a,d8,b1,ee,ea,4b,14,7a,97,12,eb,6d,f1,5b,e1,f4,\
  90,ab,5e,f0,af,71,66,82,54,5d

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Setup]
""Version""=""10.53.6000.34""
""SP""=dword:00000003
""SQLPath""=""" + path + @"\\MSSQL10_50.XSQL2008\\MSSQL""
""ProductCode""=""{B5153233-9AEE-4CD4-9D2C-4FAAC870DBE2}""
""SqlProgramDir""=""" + path + @"\\""
""FeatureList""=""SQL_Engine_Core_Inst=3 SQL_DataFiles_Core_Inst=3 SQL_ENGINE_DB_CNI=3 SQL_CMDLINETOOLS_CNI=3 SQL_DUMPER_CNI=3 SQL_Engine_CNI=3 SQL_ENGINE_CORE_CNI=3 SQL_LEGACYTOOLS_CNI=3 SQL_REPL_ENGINE_SUPPORT_CNI=3 SQL_SLP_ENGINE_SUPPORT_CNI=3 MPT_AGENT_CORE_CNI=3 SQL_AGENT_FNI=3 SQL_DATA_COLLECTOR_FNI=3 SQL_LEGACYTOOLS_FNI=3 SQL_MAIL_FNI=3 SQL_REPL_ENGINE_SUPPORT_FNI=3 SQL_UPGRADESCRIPTS_FNI=3""
""PatchLevel""=""10.53.6000.34""
""SQLBinRoot""=""" + path + @"\\MSSQL10_50.XSQL2008\\MSSQL\\Binn""
""Language""=dword:00000804
""checksum""=hex:39,36,33,66,66,66,30,65,62,38,32,62,30,34,37,62,31,30,35,31,36,\
  38,32,31,30,30,63,63,36,36,33,30,30,66,62,30,30,37,66,31,62,63,35,66,65,61,\
  31,38,66,37,66,35,61,33,65,32,30,38,66,64,39,39,35,63,64,32,36,30,36,62,33,\
  37,38,34,34,64,63,39,63,38,39,36,38,31,61,38,35,33,34,32,30,30,38,30,66,65,\
  32,63,65,30,38,64,33,62,35,32,34,39,61,32,37,64,30,32,33,65,30,34,33,61,66,\
  62,31,64,64,34,31,64,62,37,61,30,62,64,34,30,61,32,30,62,62,34,32,63,30,66,\
  66,30,31,32,36,35,33,35,64,62,39,33,64,64,30,62,61,32,63,34,34,36,34,38,63,\
  65,61,31,66,31,34,64,39,38,61,31,65,63,33,31,33,62,32,35,34,38,32,34,00
""ProductID""=""02440-116-0004523-05193""
""DigitalProductID""=hex:a4,00,00,00,03,00,00,00,30,32,34,34,30,2d,31,31,36,2d,\
  30,30,30,34,35,32,33,2d,30,35,31,39,33,00,0a,00,00,00,38,31,30,2d,30,38,32,\
  35,31,00,00,00,00,00,00,00,88,0d,d4,0d,f2,38,12,9f,81,a6,25,98,2c,12,03,00,\
  00,00,00,00,2f,a8,73,57,5a,03,02,00,00,00,00,00,00,00,00,00,00,00,00,00,00,\
  00,00,00,00,00,00,00,00,00,00,00,36,37,37,32,30,00,00,00,00,00,00,00,16,0e,\
  00,00,e2,8e,1f,dc,00,04,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,\
  00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,95,56,64,97
""EditionType""=""Enterprise Edition""
""Edition""=""Enterprise Edition""
""SQLGroup""=""S-1-5-21-824995083-1919872071-3674431250-1003""
""Collation""=""Chinese_PRC_CI_AS""
""SQLDataRoot""=""" + path + @"\\MSSQL10_50.XSQL2008\\MSSQL""
""AGTGroup""=""S-1-5-21-824995083-1919872071-3674431250-1004""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Setup\SQL_Engine_Core_Inst]
""ProductCode""=""{B5153233-9AEE-4CD4-9D2C-4FAAC870DBE2}""
""FeatureList""=""SQL_Engine_Core_Inst=3 SQL_DataFiles_Core_Inst=3 SQL_ENGINE_DB_CNI=3 SQL_CMDLINETOOLS_CNI=3 SQL_DUMPER_CNI=3 SQL_Engine_CNI=3 SQL_ENGINE_CORE_CNI=3 SQL_LEGACYTOOLS_CNI=3 SQL_REPL_ENGINE_SUPPORT_CNI=3 SQL_SLP_ENGINE_SUPPORT_CNI=3 MPT_AGENT_CORE_CNI=3 SQL_AGENT_FNI=3 SQL_DATA_COLLECTOR_FNI=3 SQL_LEGACYTOOLS_FNI=3 SQL_MAIL_FNI=3 SQL_REPL_ENGINE_SUPPORT_FNI=3 SQL_UPGRADESCRIPTS_FNI=3""
""Version""=""10.53.6000.34""
""PatchLevel""=""10.53.6000.34""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\Setup\SQL_Engine_Core_Inst\2052]
""ProductCode""=""{4F0550DE-97D6-470D-B07E-9826D61FDF91}""
""FeatureList""=""SQL_Engine_Core_Inst_Loc=3 SQL_ENGINE_CORE_CLI=3 MPT_AGENT_CORE_CLI=3 SQL_AGENT_FLI=3 SQL_DATA_COLLECTOR_FLI=3 SQL_LEGACYTOOLS_FLI=3 SQL_MAIL_FLI=3""
""Version""=""10.53.6000.34""
""PatchLevel""=""10.53.6000.34""

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\SQLServerAgent]
""ErrorLogFile""=""" + path + @"\\MSSQL10_50.XSQL2008\\MSSQL\\Log\\SQLAGENT.OUT""
""WorkingDirectory""=""" + path + @"\\MSSQL10_50.XSQL2008\\MSSQL\\JOBS""
""RestartServer""=""1""
""ErrorLoggingLevel""=dword:00000003
""JobHistoryMaxRows""=dword:000003e8
""JobHistoryMaxRowsPerJob""=dword:00000064
""NonAlertableErrors""=""1204,4002""
""MSXServerName""=""""
""SysAdminOnly""=""1""
""ServerHost""=""""
""DownloadedMaxRows""=dword:00000064

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\SQLServerSCP]

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008\UpgradeIncompleteState]
""MPT_AGENT_CORE_CNI""=dword:00000000
""SQL_Engine_Core_Inst""=dword:00000000");
            saveErrLog(sb.ToString(), "install.reg", path);
            sb.Length = 0;
            sb.Append(@"Windows Registry Editor Version 5.00
[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL]
""XSQL2008""=-
[-HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\XSQL2008]
[-HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10_50.XSQL2008]");
            saveErrLog(sb.ToString(), "unstall.reg", path);
        }

        #endregion

        #region //写文件
        public static void saveErrLog(string text, string txtName, string path)
        {
            path = path + "\\" + txtName;
            File.WriteAllText(path, text);

        }
        public static void saveErrLog(string log, string txtName)
        {
            string path = System.Environment.CurrentDirectory + "\\";
            if (File.Exists(path + @"\" + txtName))
            {
                StreamWriter SW;
                SW = File.AppendText(path + @"\" + txtName);
                SW.WriteLine(log + "\r\n");
                SW.Close();
            }
            else
            {
                StreamWriter SW;
                SW = File.CreateText(path + @"\" + txtName);
                SW.WriteLine(log + "\r\n");
                SW.Close();
            }

        }
        #endregion

        /// <summary>
        /// 执行cmd命令
        /// </summary>
        /// <param name="code"></param>
        private void RunCmdCode(string code)
        {
            string str = code;

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(str + "&exit");

            p.StandardInput.AutoFlush = true;
            //p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();

            //StreamReader reader = p.StandardOutput;
            //string line=reader.ReadLine();
            //while (!reader.EndOfStream)
            //{
            //    str += line + "  ";
            //    line = reader.ReadLine();
            //}
            ApendLog(output);
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            Console.WriteLine(output);
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            var cmd = @"
                net stop ""MSSQL$XSQL2008""
                sc stop ""MSSQL$XSQL2008""
                sc config ""MSSQL$XSQL2008"" start= disabled
                sc delete ""MSSQL$XSQL2008""
                reg import """ + path + @"\unstall.reg""
                c:\Windows\SysWOW64\reg.exe import """ + path + @"\unstall.reg""";
            RunCmdCode(cmd);
            ApendLog("卸载完成！");
            Application.Restart();
        }

        private void btnUpdatePWD_Click(object sender, EventArgs e)
        {
            UpdatePWD form = new UpdatePWD();
            form.ShowDialog();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            BackupDB form = new BackupDB();
            form.ShowDialog();
        }

        private void btnRestoreDB_Click(object sender, EventArgs e)
        {
            RestoreDB form = new RestoreDB();
            form.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.microsoft.com/zh-cn/download/details.aspx?id=7593");

        }

        private void btnAttachDB_Click(object sender, EventArgs e)
        {
            AttachDB form = new AttachDB();
            form.ShowDialog();
        }

        private void btnSqlProfiler_Click(object sender, EventArgs e)
        {

        }
        private ArrayList plugins = new ArrayList();
        //private System.Windows.Forms.MenuItem menuItem6;
        private ArrayList piProperties = new ArrayList();
        /// <summary>
        /// CSPluginKernel.IPlugin  可以用XML来配置，以实现动态
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool IsValidPlugin(Type t)
        {
            bool ret = false;
            Type[] interfaces = t.GetInterfaces();
            foreach (Type theInterface in interfaces)
            {
                if (theInterface.FullName == "CSPluginKernel.IPlugin")
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }
        private void LoadAllPlugins()
        {
            string[] files = Directory.GetFiles(Application.StartupPath + "\\plugins\\");
            int i = 0;
            PluginInfoAttribute typeAttribute = new PluginInfoAttribute();
            foreach (string file in files)
            {
                string ext = file.Substring(file.LastIndexOf("."));
                if (ext != ".dll") continue;
                try
                {
                    Assembly tmp = Assembly.LoadFile(file);
                    Type[] types = tmp.GetTypes();
                    bool ok = false;
                    foreach (Type t in types)
                        if (IsValidPlugin(t))
                        {
                            plugins.Add(tmp.CreateInstance(t.FullName));
                            object[] attbs = t.GetCustomAttributes(typeAttribute.GetType(), false);
                            PluginInfoAttribute attribute = null;
                            foreach (object attb in attbs)
                            {
                                if (attb is PluginInfoAttribute)
                                {
                                    attribute = (PluginInfoAttribute)attb;
                                    attribute.Index = i;
                                    i++;
                                    ok = true;
                                    break;
                                }
                            }

                            if (attribute != null) this.piProperties.Add(attribute);
                            else throw new Exception("未定义插件属性");

                            if (ok) break;
                        }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            foreach (PluginInfoAttribute pia in piProperties)
            {


                pia.Tag = AddContextMenu(pia.Name, 插件ToolStripMenuItem.DropDownItems, new EventHandler(MenuClicked));
            }

            foreach (IPlugin pi in plugins)
            {
                if (pi.Connect((IApplicationObject)this) == ConnectionResult.Connection_Success)
                {
                    pi.OnLoad();
                }
                else
                {
                    MessageBox.Show("Can not connect plugin!");
                }
            }
        }

        /// <summary>
        /// 添加子菜单
        /// </summary>
        /// <param name="text">要显示的文字，如果为 - 则显示为分割线</param>
        /// <param name="cms">要添加到的子菜单集合</param>
        /// <param name="callback">点击时触发的事件</param>
        /// <returns>生成的子菜单，如果为分隔条则返回null</returns>

        ToolStripMenuItem AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback)
        {
            if (text == "-")
            {
                ToolStripSeparator tsp = new ToolStripSeparator();
                cms.Add(tsp);
                return null;
            }
            else if (!string.IsNullOrEmpty(text))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(text);
                tsmi.Tag = text + "TAG";
                if (callback != null) tsmi.Click += callback;
                cms.Add(tsmi);

                return tsmi;
            }

            return null;
        }

        void MenuClicked(object sender, EventArgs e)
        {
            foreach (PluginInfoAttribute pia in piProperties)
                if (pia.Tag.Equals(sender))
                    ((IPlugin)plugins[pia.Index]).Run();

        }

        #region IApplicationObject 成员

        public void SetDelegate(Delegates whichOne, EventHandler target)
        {
            //switch (whichOne)
            //{
            //    case Delegates.Delegate_ActiveDocumentChanged:
            //        this.tabDocs.SelectedIndexChanged += target;
            //        break;
            //}
        }

        public IDocumentObject[] QueryDocuments()
        {
            ArrayList list = new ArrayList();
            //for (int i = 0; i < this.tabDocs.TabPages.Count; i++)
            //    list.Add(tabDocs.TabPages[i].Tag);
            return (IDocumentObject[])list.ToArray();
        }

        public IDocumentObject QueryCurrentDocument()
        {
            //if (tabDocs.SelectedIndex != -1)
            //    return (IDocumentObject)this.tabDocs.SelectedTab.Tag;
            //else
            return null;
        }

        public void ShowInStatusBar(string msg)
        {
            // _Status.Panels[0].Text = msg;
        }

        public void Alert(string msg)
        {
            MessageBox.Show(msg);
        }
        #endregion

        private void 插件设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = "lxerp.ini";  //测试一个word文档
            System.Diagnostics.Process.Start(path); //打开此文件。
        }

        private void 版权申明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"本软件的部分内容是在网上搜集，
如有侵犯你的权利请email我删除。
使用者可将本软件提供的内容用于个人学习、研究或欣赏，
以及其他非商业性或非盈利性用途，
但同时应遵守著作权法及其他相关法律的规定，
不得侵犯本软件及相关权利人的合法权利。
除此以外，将本软件任何内容或服务用于其他用途时，
须征得相关权利人的书面许可。");
        }

        private void btnOpenIsqlw_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "SQLchaxunfx\\isqlw.exe";  //
                System.Diagnostics.Process.Start(path); //打开此文件。
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
            }
        }

        private void 捐赠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Donate form = new Donate();
            form.ShowDialog();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("xiefengdaxia123@163.com");
        }

    }
}
