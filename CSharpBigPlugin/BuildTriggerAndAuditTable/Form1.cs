using CSPluginKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BuildTriggerAndAuditTable
{
    public partial class Form1 : Form
    {
        private IDocumentObject _Obj;
        public Form1(IDocumentObject obj)
        {
            InitializeComponent();
            _Obj = obj;
            //Control.CheckForIllegalCrossThreadCalls = false;
            CSHelper.getsqlconn();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string[] tableArray = richTextBox3.Text.Split('\n');
            for (int i = 0; i < tableArray.Length; i++)
            {
                if (tableArray[i].Trim() != "")
                {
                   
                    saveErrLog(createAuditTable(tableArray[i]) + "\nGO\n" + createTrigger(tableArray[i], ""), tableArray[i], "sql");

                }
            }
            //完成后自动打开生成的脚本文件夹
            if (Directory.Exists(Application.StartupPath + "\\审计") != false)
            {
                string path = Application.StartupPath + "\\审计"; System.Diagnostics.Process.Start("explorer.exe", path);
            }
        }
        public void saveErrLog(string log, string txtName, string expandedName)
        {
            log = "--" + DateTime.Now.ToString() + " : \n" + log;
            if (Directory.Exists(Application.StartupPath + "\\审计\\") == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(Application.StartupPath + "\\审计\\");
            }
            //if (File.Exists(Application.StartupPath + "\\审计\\" + txtName + ".sql"))
            //{
            //    StreamWriter SW;
            //    SW = File.AppendText(Application.StartupPath + "\\审计\\" + txtName + ".sql");
            //    SW.WriteLine(log + "\r\n");
            //    SW.Close();
            //    //Console.WriteLine("Text Appended Successfully");
            //}
            //else
            //{
            StreamWriter SW;
            SW = File.CreateText(Application.StartupPath + "\\审计\\" + txtName + ".sql");
            SW.WriteLine(log + "\r\n");
            SW.Close();
            listBox1.Items.Add("" + txtName + ".sql已经重新创建！");
            //}
        }
        public DataTable getTableDesign(string table_name)
        {
           //CSHelper.getsqlconn();
           SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT   d.name
                表名 ,
        a.colorder 字段序号 ,
        a.name 字段名 ,
        ( CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 THEN '√'
               ELSE ''
          END ) 标识 ,
        ( CASE WHEN ( SELECT    COUNT(*)
                      FROM      sysobjects
                      WHERE     ( NAME IN (
                                  SELECT    NAME
                                  FROM      sysindexes
                                  WHERE     ( id = a.id )
                                            AND ( indid IN (
                                                  SELECT    indid
                                                  FROM      sysindexkeys
                                                  WHERE     ( id = a.id )
                                                            AND ( colid IN (
                                                              SELECT
                                                              colid
                                                              FROM
                                                              syscolumns
                                                              WHERE
                                                              ( id = a.id )
                                                              AND ( NAME = a.name ) ) ) ) ) ) )
                                AND ( xtype = 'PK' )
                    ) > 0 THEN '√'
               ELSE ''
          END ) 主键 ,
        b.name 类型 ,
        a.length 占用字节数 ,
        COLUMNPROPERTY(a.id, a.name, 'PRECISION') AS 长度 ,
        ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数 ,
        ( CASE WHEN a.isnullable = 1 THEN '√'
               ELSE ''
          END ) 允许空 ,
        ISNULL(e.text, '') 默认值
       FROM    syscolumns a
        LEFT JOIN systypes b ON a.xtype = b.xusertype
        INNER JOIN sysobjects d ON a.id = d.id
                                   AND d.xtype = 'U'
                                   AND d.name <> 'dtproperties'
        LEFT JOIN syscomments e ON a.cdefault = e.id ";
            cmd.CommandText += (" WHERE d.name ='" + table_name + "'");

            cmd.CommandText += " ORDER BY a.id ,a.colorder";
            SqlDataAdapter huoche = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            huoche.Fill(dt);
            return dt;
        }
        public string createAuditTable(string table_name)
        {
            var sqlText = new StringBuilder();
            DataRow[] datarows = getTableDesign(table_name).Select();
            sqlText.AppendLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Audit_" + table_name + "]'))");
            sqlText.AppendLine("drop table [dbo].[Audit_" + table_name + "]");
            sqlText.AppendLine("GO");
            sqlText.AppendLine("CREATE TABLE [dbo].[Audit_" + table_name + "](");
            sqlText.AppendLine("[auditID] [numeric](18, 0) PRIMARY KEY IDENTITY(1,1) NOT NULL,");
            sqlText.AppendLine("[action] [varchar](50) NULL,");
            sqlText.AppendLine("[from] [varchar](100) NULL,");
            sqlText.AppendLine("[auditTimestamp] [datetime] NULL,");
            foreach (DataRow item in datarows)
            {
                var type = item["类型"].ToString();
                if (type.Contains("ar"))
                {
                    sqlText.AppendLine("[" + item["字段名"] + "] [" + type + "](" + item["长度"] + ") NULL,");
                }
                else if (type.Contains("dec") || type.Contains("num"))
                {
                    sqlText.AppendLine("[" + item["字段名"] + "] [" + type + "](" + item["长度"] + "," + item["小数位数"] + ") NULL,");
                }
                else
                {
                    sqlText.AppendLine("[" + item["字段名"] + "] [" + type + "] NULL,");
                }
            }
            //去掉最后一个逗号
            //逗号位置
            var index = sqlText.ToString().LastIndexOf(",");
            if (index != -1)
            {
                var sb = sqlText.ToString().Substring(0, index);
                sqlText.Length = 0;
                sqlText.Append(sb + ")");
            }

            return sqlText.ToString();
        }
        public string createTrigger(string table_name, string audit_table_name)
        {
            //合成的字段
            var sbField = new StringBuilder();

            //创建触发器的sql
            var sb_all = new StringBuilder();

            DataRow[] datarows = getTableDesign(table_name).Select();
            foreach (DataRow item in datarows)
            {
                sbField.Append(item["字段名"] + ",");
            }
            if (sbField.Length<= 0)
            {
                MessageBox.Show(table_name+"表，字段为空，请检查是否含有该表");
                return "";
            }
            //去掉最后一个逗号
            sbField.Length = sbField.Length - 1;
            sb_all = build(table_name, 0, sbField);
            sb_all.AppendLine(build(table_name, 1, sbField).ToString());
            sb_all.AppendLine(build(table_name, 2, sbField).ToString());
            return sb_all.ToString();
        }
        public StringBuilder build(string table_name, int type, StringBuilder sbField)
        {
            StringBuilder sb = new StringBuilder();
            string action = string.Empty;
            string insert_sql = string.Empty;
            switch (type)
            {
                case 0: { action = "INSERT"; insert_sql = "INSERT INTO Audit_" + table_name + " SELECT '" + action + "',@from,GETDATE()," + sbField + " FROM INSERTED"; } break;
                case 1: { action = "UPDATE"; insert_sql = "INSERT INTO Audit_" + table_name + " SELECT '" + action + "',@from,GETDATE()," + sbField + " FROM INSERTED"; } break;
                case 2: { action = "DELETE"; insert_sql = "INSERT INTO Audit_" + table_name + " SELECT '" + action + "',@from,GETDATE()," + sbField + " FROM DELETED"; } break;
            }
            sb.AppendLine("IF EXISTS(select * from sysobjects where type='tr' and name='TGR_" + table_name + "_" + action + "')");
            sb.AppendLine("DROP  TRIGGER TGR_" + table_name + "_" + action);
            sb.AppendLine("GO");
            sb.AppendLine("CREATE TRIGGER TGR_" + table_name + "_" + action);
            sb.AppendLine("ON [" + table_name + "] FOR " + action);
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("DECLARE @from VARCHAR(100)");
            sb.AppendLine("SELECT @from=(SELECT '客户机名:'+rtrim(hostname)+' 程序名:'+program_name FROM master..sysprocesses WHERE spid=(SELECT @@SPID))");
            sb.AppendLine(insert_sql);
            sb.AppendLine("End");
            sb.AppendLine("GO");
            //action = null;
            //insert_sql = null;
            return sb;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
