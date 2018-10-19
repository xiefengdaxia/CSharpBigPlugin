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
using System.Threading;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
namespace BuildAndExecuteSQL
{
    public partial class Form1 : Form
    {
        private IDocumentObject _Obj;
        public Form1(IDocumentObject obj)
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            _Obj = obj;
        }

        private void button22_Click(object sender, EventArgs e)
        {

        }

        //清除数据
        private void button13_Click(object sender, EventArgs e)
        {
            DialogResult Dloresult = MessageBox.Show("是否要清空 "+textBox12.Text+" 表的数据?\n\r" + textBoxConnectionString.Text + "\n\r正确点确定，否则取消", "友情提示", MessageBoxButtons.OKCancel);
            if (Dloresult != DialogResult.OK)
            {
                return;
            }
            var sql = "TRUNCATE TABLE " + textBox12.Text + " ;";
            var result = CSHelper.exec_sql(sql);
            if (result == -1)
                MessageBox.Show("清除成功！");
        }
         //将转为字典，然后调用服务器的同种方法把字典转为sql
        private string GetOneRowSql(DataSet schema, DataRow row, string tableName)
        {

            Dictionary<string, Object> obj = new Dictionary<string, object>();
            obj.Add("data_type", tableName);
            foreach (DataColumn col in schema.Tables[0].Columns)
            {
                obj.Add(col.ColumnName, row[col.ColumnName]);
            }
            return DictToInsertSQL(obj);
        }

        //是否包含了默认的key
        //如果是返回true
        //遇到contine跳过当前循环
        public static bool ifHasDefaultKeys(string key)
        {
            return (key == "LocalId" || key == "sync_type" || key == "data_type" || key == "sync_id" || key == "timestamp" || key == "new_timestamp");
        }

        /// <summary>
        /// 根据字典生成sql插入语句
        /// </summary>
        /// <param name="dict"></param>
        /// <returns>sql插入语句</returns>
        public static string DictToInsertSQL(Dictionary<string, Object> dict)
        {
            StringBuilder sqlText = new StringBuilder();
            //sqlText.AppendLine("SET IDENTITY_INSERT  " + dict["data_type"] + " on ");
            sqlText.AppendFormat("Insert {0} (", dict["data_type"]);
            int i = 0;
            foreach (string key in dict.Keys)
            {
                if (ifHasDefaultKeys(key))
                    continue;
                if (i == 0)
                {
                    sqlText.AppendFormat("[{0}]", key.ToString());
                }
                else
                {
                    sqlText.AppendFormat(", [{0}]", key.ToString());
                }
                i++;
            }
            sqlText.Append(") Values(");
            i = 0;
            foreach (string key in dict.Keys)
            {

                if (ifHasDefaultKeys(key))
                    continue;
                if (i == 0)
                {

                    sqlText.AppendFormat("{0}", toNull(dict, key));
                }
                else
                {
                    sqlText.AppendFormat(",{0}", toNull(dict, key));
                }

                i++;
            }
            sqlText.Append(");");
            //sqlText.AppendLine("SET IDENTITY_INSERT  " + dict["data_type"] + " off ");
            return sqlText.ToString();
        }

        public static string toNull(Dictionary<string, Object> dict, string key)
        {
            string returnValue = dict[key].ToString();
            if (dict[key].ToString().Length == 0)
            {
                return "null";
            }
            if (dict[key].GetType() == typeof(string))
            {
                returnValue = string.Format("'{0}'", dict[key].ToString().Replace("'", ""));//有的值有'导致生成的sql语法不规范
            }
            else if (dict[key].GetType() == typeof(DateTime))
            {
                returnValue = string.Format("'{0}'", dict[key]);
            }
            return returnValue;
        }
        /// <summary>
        /// 创建sql脚本
        /// </summary>
        /// <param name="sql">sql查询语句</param>
        /// <param name="table_name">表名</param>
        /// <param name="txtbox"></param>
        /// <param name="isChecked">是否勾上</param>
        public void createSQLScript(string sql, string table_name, TextBox txtbox, bool isChecked)
        {
            if (isChecked == false)
            {
                return;
            }
            try
            {
                using (Database db = new Database())
                {
                    db.Connect(CSHelper.sqlconn);

                    DataSet ds = db.GetSchema(sql);

                    int totalRecords = int.MaxValue;

                    //if (numericUpDownTotalRecords.Value > 0)
                    //{
                    //    totalRecords = (int)numericUpDownTotalRecords.Value;
                    //}

                    db.OpenDataReader(sql);

                    DataRow row = db.GetNextRow();
                    if (Directory.Exists(Application.StartupPath + "\\脚本") == false)
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\脚本");
                    }
                    //if (File.Exists(Application.StartupPath + "\\脚本\\" + table_name + ".sql") == false)
                    //{
                    //    File.Create(Application.StartupPath + "\\脚本\\" + table_name + ".sql");
                    //}
                    using (System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "\\脚本\\" + table_name + ".sql", System.IO.FileMode.Create,
                         System.IO.FileAccess.ReadWrite))
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.UTF8))
                        {

                            int current = 0;
                            while (row != null && totalRecords > 0)
                            {
                                totalRecords--;
                                string line = GetOneRowSql(ds, row, table_name);

                                db.Clear();

                                sw.WriteLine(line);
                                current++;
                                txtbox.Text = current.ToString();
                                Application.DoEvents();

                                row = db.GetNextRow();

                                if ((totalRecords % 100) == 0)
                                {
                                    GC.Collect();
                                }
                            }
                        }
                    }
                }

                richTextBox1.AppendText("成功创建:" + table_name + " sql脚本！\n\r");
                richTextBox1.ScrollToCaret();
            }
            catch (Exception e1)
            {
                richTextBox1.AppendText(DateTime.Now.ToLongTimeString() + ":" + e1.Message + "\n\r" + e1.StackTrace + "\n\r");
            }
        }
        private void button22_Click_1(object sender, EventArgs e)
        {
            Thread worker = new Thread(delegate()
             {
                 createSQLScript(richTextBox2.Text, textBox12.Text, textBox11, true);
                 //完成后自动打开生成的脚本文件夹
                 if (Directory.Exists(Application.StartupPath + "\\脚本") != false)
                 {
                     string path = Application.StartupPath + "\\脚本"; System.Diagnostics.Process.Start("explorer.exe", path);
                 }
             });
            worker.IsBackground = true;
            worker.Start();
        }
        string[] path = null;
        public long linecount = 0;

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("请检查连接参数是否正确?\n\r" + textBoxConnectionString.Text + "\n\r正确点确定，否则取消", "友情提示", MessageBoxButtons.OKCancel);
            if (result != DialogResult.OK)
            {
                return;
            }

            //每次批量执行的sql条数
            int sql_count = (int)numericUpDownTotalRecords.Value;

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "打开";
                ofd.Filter = "所有文件|*.sql";
                ofd.Multiselect = true;
                ofd.InitialDirectory = Application.StartupPath;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    path = ofd.FileNames;
                }
                if (path == null)
                {
                    return;
                }
                Thread worker = new Thread(delegate()
                {
                    DateTime dt_read_txt = DateTime.Now;
                    StringBuilder sqls = new StringBuilder();
                    linecount = 0;
                    for (int i = 0; i < path.Length; i++)
                    {
                        using (StreamReader sr = new StreamReader(path[i]))
                        {

                            string line = string.Empty;
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line.Trim() != "")
                                {
                                    if (!line.EndsWith(";"))
                                    {
                                        sqls.Append(line);
                                        continue;
                                    }
                                    else
                                        sqls.Append(line + "\n");

                                    linecount++;
                                    labelCurrent.Text = "导入计数:" + linecount;
                                    if (linecount % sql_count == 0)
                                    {
                                        insertToDB(sqls);
                                        sqls.Remove(0, sqls.Length);
                                        GC.Collect();
                                    }
                                }
                            }
                            if (sqls.Length > 10)
                            {
                                insertToDB(sqls);
                            }
                            labelCurrent.Text = "导入计数:" + linecount;
                            sqls.Remove(0, sqls.Length);
                            GC.Collect();
                            richTextBox1.AppendText("时间:" + DateTime.Now.ToLongTimeString() + ":" + path[i] + "执行完毕！\n\r");
                        }
                    }
                });
                worker.IsBackground = true;
                worker.Start();
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(ex.Message + "\n\r" + ex.StackTrace);
            }
        }
        public void insertToDB(StringBuilder sb_sqls,string connStr=null)
        {
            SqlConnection conn;
            if (string.IsNullOrEmpty(connStr))
            {
                conn = new SqlConnection(textBoxConnectionString.Text);
            }
            else
            {
                conn = new SqlConnection(connStr);
            }
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                cmd.CommandText = sb_sqls.ToString();
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
                richTextBox1.AppendText(ex.Message + "\n\r" + ex.StackTrace);
                tran.Rollback();
             CSHelper.saveErrLog(sb_sqls.ToString(), "批量执行失败的sql");
            }
            finally
            {
                conn.Close();
                tran.Dispose();
                conn.Dispose();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           CSHelper.getsqlconn();
        }

        private void btnSelectAndExecSql_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("请检查连接参数是否正确?\n\r" + txtBatchSqlConnStr.Text + "\n\r正确点确定，否则取消", "友情提示", MessageBoxButtons.OKCancel);
            if (result != DialogResult.OK)
            {
                return;
            }

            //每次批量执行的sql条数
            int sql_count = (int)numericUpDownTotalRecords.Value;

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "打开";
                ofd.Filter = "所有文件|*.sql";
                ofd.Multiselect = true;
                ofd.InitialDirectory = Application.StartupPath;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    path = ofd.FileNames;
                }
                if (path == null)
                {
                    return;
                }
                Thread worker = new Thread(delegate()
                {
                    
                        DateTime dt_read_txt = DateTime.Now;

                        for (int i = 0; i < path.Length; i++)
                        {
                            try
                            {
                                using (StreamReader sr = new StreamReader(path[i], Encoding.Default))
                                {
                                     //当前文件信息
                                    FileInfo currentFile = new FileInfo(path[i]);

                                    var fileInfo = string.Format("--当前脚本 修改时间：{0} 执行时间：{1} --来自批量执行脚本工具附加信息" + System.Environment.NewLine, currentFile.LastWriteTime, DateTime.Now);
                                    var sqlcontent = sr.ReadToEnd();
                                    sqlcontent = sqlcontent.Replace("CREATE PROCEDURE", fileInfo + System.Environment.NewLine + "CREATE PROCEDURE");
                                    sqlcontent = sqlcontent.Replace("create procedure", fileInfo + System.Environment.NewLine + "CREATE PROCEDURE");
                                    sqlcontent = sqlcontent.Replace("CREATE FUNCTION", fileInfo + System.Environment.NewLine + "CREATE FUNCTION");
                                    
                                    //var fileInfo = string.Format("--当前脚本 修改时间：{0} 执行时间：{1} --来自批量执行脚本工具附加信息\n\r", currentFile.LastWriteTime, DateTime.Now);
                                    //var sqlcontent = sr.ReadToEnd();
                                    //sqlcontent = sqlcontent.Replace("CREATE PROCEDURE", fileInfo + "\nCREATE PROCEDURE");
                                    //sqlcontent = sqlcontent.Replace("create procedure", fileInfo + "\nCREATE PROCEDURE");
                                    //sqlcontent = sqlcontent.Replace("CREATE FUNCTION", fileInfo + "\nCREATE FUNCTION");
                                   
                                    using (SqlConnection conn = new SqlConnection(txtBatchSqlConnStr.Text))
                                    {
                                        Microsoft.SqlServer.Management.Smo.Server server = new Server(new ServerConnection(conn));
                                        var ret = server.ConnectionContext.ExecuteNonQuery(sqlcontent);
                                        richTextBox1.AppendText("时间:" + DateTime.Now.ToLongTimeString() + ":" + path[i] + "执行结果！"+ret+"\n\r");
                                    }
                                    GC.Collect();

                                }
                            }
                            catch (Exception ex)
                            {
                                richTextBox1.AppendText("执行" + path[i] + "报错！"+ex.Message + "\n\r" + ex.StackTrace);
                            }
                        }
                   
                });
                worker.IsBackground = true;
                worker.Start();
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(ex.Message + "\n\r" + ex.StackTrace);
            }
        }
    }
}
