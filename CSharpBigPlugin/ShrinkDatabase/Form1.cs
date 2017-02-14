using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ShrinkDatabase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int selectRow = 0;
        public string lastTable = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            CSHelper.getsqlconn();

            //不加这句话，有时候不按设计的列来排序，而且线程更新时设计的style没有了
            this.dgvTable.AutoGenerateColumns = false;
            dgvTable.EnableHeadersVisualStyles = false;
            dgvTable.Columns["unuseSpace"].HeaderCell.Style.BackColor = Color.PaleTurquoise;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowTableInfo(selectRow);
        }

        public void ShowTableInfo(int selectRow)
        {

            //耗时操作在后台进程进行
            Thread worker = new Thread(delegate()
              {
                  try
                  {
                      //dgvTable.DataSource = null;
                      ShowLoading(true);
                      ApendLog("正在刷新列表");
                      string sql = @"if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tableinfo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
                drop table [dbo].tableinfo
                CREATE TABLE [dbo].tableinfo(
                 表名 [varchar](50) NULL,
                 记录数 [int] NULL,
                 预留空间 [varchar](50)  NULL,
                 使用空间 [varchar](50)  NULL,
                 索引占用空间 [varchar](50)  NULL,
                 未用空间 [varchar](50) NULL
                )";
                      CSHelper.exec_sql(sql);
                      sql = "insert into tableinfo(表名, 记录数, 预留空间, 使用空间, 索引占用空间, 未用空间) ";
                      sql += "exec sp_MSforeachtable \"exec sp_spaceused '?'\"";
                      CSHelper.exec_sql(sql);

                      sql = " select top 40 * from tableinfo with(nolock) order by 记录数 desc ";


                      SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
                      conn.Open();

                      SqlDataAdapter huoche = new SqlDataAdapter(sql, conn);
                      DataSet ds = new DataSet();
                      huoche.Fill(ds, "tableinfo");
                      UpdateGV(ds.Tables["tableinfo"], selectRow);
                      conn.Close();
                      conn.Dispose();
                  }
                  catch (Exception ex)
                  {
                      ApendLog("显示列表出错啦" + ex.Message + "\n" + ex.StackTrace);
                      //MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "显示列表出错啦");
                  }
                  finally
                  {
                      ShowLoading(false);
                      ApendLog("刷新列表完成");
                  }
              });
            worker.IsBackground = true;
            worker.Start();







        }
        /// <summary>
        /// 后台线程更新dataGridView
        /// </summary>
        /// <param name="dt"></param>
        private delegate void UpdateDataGridView(DataTable dt, int selectRow);
        private void UpdateGV(DataTable dt, int selectRow)
        {
            if (dgvTable.InvokeRequired)
            {
                this.Invoke(new UpdateDataGridView(UpdateGV), new object[] { dt, selectRow });
            }
            else
            {
                dgvTable.DataSource = dt;
                dgvTable.Refresh();
                if (selectRow != 0)
                {
                    dgvTable.Rows[selectRow].Selected = true;
                    dgvTable.FirstDisplayedScrollingRowIndex = selectRow;
                    //ApendLog(selectRow.ToString());
                    //非常奇怪的问题，当前行数与选择的行索引不一样
                    UpdateCurrentTableName(dgvTable.Rows[selectRow].Cells["tableName"].Value.ToString());
                }
            }

            //Invoke(new MethodInvoker(delegate()
            //{
            //    dgvTable.DataSource = dt;
            //    dgvTable.FirstDisplayedScrollingRowIndex = selectRow;
            //    UpdateCurrentTableName(dgvTable.Rows[selectRow].Cells["tableName"].Value.ToString());
            //}));
        }

        /// <summary>
        /// 显示loading加载图
        /// </summary>
        /// <param name="ifshow"></param>
        public void ShowLoading(bool ifshow)
        {
            //无参数,但是返回值为bool类型
            this.Invoke(new Func<bool>(delegate()
            {
                pictureBox1.Visible = ifshow;
                return true; //返回值
            }));

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

        public void UpdateCurrentTableName(string tableName)
        {
            this.Invoke(new Func<bool>(delegate()
            {
                CurrentTableName.Text = tableName;
                return true;
            }));
        }
        private void dgvTable_SelectionChanged(object sender, EventArgs e)
        {
            //this.dgvTable.ClearSelection();
            if (dgvTable.RowCount > 1)
            {
                //MessageBox.Show(dgvTable.CurrentRow.Cells.ToString());
                if (dgvTable.CurrentRow.Cells["tableName"].Value != null)
                {
                    CurrentTableName.Text = dgvTable.CurrentRow.Cells["tableName"].Value.ToString();
                    selectRow = dgvTable.CurrentRow.Index;
                }
            }

        }

        public bool isRuning = true;
        private void button2_Click(object sender, EventArgs e)
        {
            //如果没有选中表，则不执行
            if (string.IsNullOrEmpty(CurrentTableName.Text))
                return;

            //是否含有聚集索引
            bool hasClusteredIndex = (CSHelper.ifExist("select count(*) from sys.indexes where object_id=OBJECT_ID('" + CurrentTableName.Text + "') and type_desc='CLUSTERED'; ")==1);
            //if (!hasClusteredIndex)
            //{
            //    MessageBox.Show("此表没有聚集索引，请先创建！");
            //    return;
            //}


            Thread worker = new Thread(delegate()
                {
                    Stopwatch sw = new Stopwatch();
                    try
                    {
                        sw.Start();
                        ShowLoading(true);

                        if (hasClusteredIndex)
                        {
                            ApendLog(string.Format("正在对表[{0}]重建索引", CurrentTableName.Text));
                            CSHelper.exec_sql(string.Format("DBCC DBREINDEX ({0}, '', 90)  ", CurrentTableName.Text));
                        }
                        else
                        {
                            ApendLog(string.Format("准备对表[{0}]建立索引", CurrentTableName.Text));

                            var field = CSHelper.ExecuteScalar(string.Format("Select top 1 name from syscolumns Where ID=OBJECT_ID('{0}')", CurrentTableName.Text));
                            ApendLog(string.Format("查出表[{0}]第一个字段为：{1}", CurrentTableName.Text, field));
                            ApendLog(string.Format("正在为表[{0}]字段[{1}]建立临时ClusteredIndex索引...时间比较久", CurrentTableName.Text, field));
                            CSHelper.exec_sql(string.Format("create clustered index ClusteredIndex on {0}({1}) ", CurrentTableName.Text, field));
                           
                            ApendLog(string.Format("删除临时索引中..."));
                            CSHelper.exec_sql(string.Format("DROP INDEX {0}.ClusteredIndex ", CurrentTableName.Text, field));
                        }
                    }
                    catch (Exception ex)
                    {
                        ApendLog(ex.Message);
                    }
                    finally
                    {
                        sw.Stop();
                        TimeSpan ts = sw.Elapsed;
                        ApendLog("重建索引完成,耗时:" + string.Format("{0}时{1}分{2}秒{3}毫秒", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds));
                        isRuning = false;
                    }
                });
            worker.IsBackground = true;
            worker.Start();
            t = new System.Timers.Timer(3000);
            t.Elapsed += t_Elapsed;
            t.Enabled = true;
        }
        System.Timers.Timer t;
        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //ApendLog("计时器判断重建索引状态:" + isRuning);
            if (!isRuning)
            {
                ApendLog("计时器开始执行显示列表！");
                //ShowTableInfo(selectRow);
                button1_Click(sender, e);
                t.Enabled = false;
                ApendLog("计时器关闭！");
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {

            var Database = CSHelper.ReadINI("Connection", "Database");
            if (!string.IsNullOrEmpty(Database))
            {
                Thread worker = new Thread(delegate()
                {
                    Stopwatch sw = new Stopwatch();
                    try
                    {
                        sw.Start();
                        ShowLoading(true);
                        ApendLog(string.Format("正在对数据库[{0}]进行收缩", Database));
                        ApendLog("-----------------------收缩前大小----------------------------------");
                        countDatabaseSize(Database);
                        CSHelper.exec_sql(string.Format("DBCC SHRINKDATABASE ({0})  ", Database));

                    }
                    catch (Exception ex)
                    {
                        ApendLog(ex.Message);
                    }
                    finally
                    {
                        sw.Stop();
                        TimeSpan ts = sw.Elapsed;
                        ApendLog("数据库收缩完成,耗时:" + string.Format("{0}时{1}分{2}秒{3}毫秒", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds));
                        ApendLog("-----------------------收缩后大小----------------------------------");
                        countDatabaseSize(Database);
                        ShowLoading(false);
                    }
                });
                worker.IsBackground = true;
                worker.Start();

            }
        }
        /// <summary>
        /// 查询数据库文件大小
        /// </summary>
        /// <param name="dbname"></param>
        public void countDatabaseSize(string dbname)
        {
            //SqlServer是以8k为一页
            var sql = "select name, convert(float,size) * (8192.0/1024.0)/1024. size from " + dbname + ".dbo.sysfiles";
            SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ApendLog("文件名:" + dr.GetValue(0).ToString() + " 大小:" + dr.GetValue(1).ToString() + "M");
                }
            }
            catch (Exception ex)
            {

                CSHelper.saveErrLog(sql + ex.Message, DateTime.Now.ToString("yyyy-MM-dd") + "-sql_err");

            }

            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}