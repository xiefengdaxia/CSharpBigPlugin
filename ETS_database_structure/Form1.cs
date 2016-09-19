using CSPluginKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ETS_database_structure
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

        //上次检索的表
        private string lastWord;

        private void Form1_Load(object sender, EventArgs e)
        {
            lastWord = CSHelper.ReadINI("Connection", "LastWord");
            txtTableName.Text = lastWord;
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
        //生成文档
        public void bulidMarkdown()
        {
            //显示加载
            ShowLoading(true);
            try
            {
                string sql = "";
                sql += @"SELECT   d.name
                表名 ,f.remark 表备注,
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
        ISNULL(e.text, '') 默认值,
        CASE WHEN (SELECT g.remark FROM t_table_field g WHERE g.table_name=d.NAME AND g.table_field=a.name) IS NOT NULL THEN (SELECT g.remark FROM t_table_field g WHERE g.table_name=d.NAME AND g.table_field=a.name) ELSE (SELECT pbc_cmnt FROM dbo.pbcatcol WHERE pbc_tnam=d.name AND pbc_cnam=a.name) END 备注
FROM    syscolumns a
        LEFT JOIN systypes b ON a.xtype = b.xusertype
        INNER JOIN sysobjects d ON a.id = d.id
                                   AND d.xtype = 'U'
                                   AND d.name <> 'dtproperties'
        LEFT JOIN syscomments e ON a.cdefault = e.id 
        LEFT JOIN t_table_name f ON d.NAME=f.table_name";
                if (rtnCurrentTable.Checked == true)
                    sql += " WHERE d.name = '" + label4.Text.Trim() + "'";
                else
                    //sql += " WHERE d.name IN(SELECT DISTINCT pbc_tnam FROM pbcatcol )";
                    sql += " WHERE d.name IN(" + textBox4.Text.Trim() + ")";
                if (rbtAllTable.Checked)
                    sql += " ORDER BY PATINDEX('% ' + CONVERT(nvarchar(4000), d.name) + ' %', ' ' + CONVERT(nvarchar(4000), Replace('" + textBox4.Text.Trim().Replace("'", "") + "', ',',' , ')) + ' ')";
                else
                    sql += @"ORDER BY a.id ,a.colorder";
                SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                StringBuilder sb = new StringBuilder();

                if (!chkXML.Checked && rbtAllTable.Checked)
                {
                    sb.AppendLine("[toc]");
                    sb.AppendLine("##新产品后台数据库结构");
                }
                else if (chkXML.Checked)
                    sb.AppendLine(Properties.Resources.headtext);

                int i = 0;
                string lastTableName = "";
                // var iflastflied = false;
                while (dr.Read())
                {
                    //每个表的头部内容
                    if (i == 0 || lastTableName != dr["表名"].ToString())
                    {
                        if (!chkXML.Checked)
                        {
                            sb.AppendLine("\n");
                            sb.AppendLine("###" + dr["表备注"] + " " + dr["表名"]);
                            sb.AppendLine("|字段|类型|主键|标识|允许空|默认值|备注|");
                            sb.AppendLine("| :-------- | --------:| :--: | :--: | :--: | :--: | :--: |");
                        }
                        else
                        {
                            if (i != 0)
                            {
                                sb.AppendLine(" </UML:Classifier.feature>");
                                sb.AppendLine("</UML:Class>");
                            }
                            sb.AppendLine("\n");
                            sb.AppendLine("                    <UML:Class name=\"" + dr["表名"] + " " + dr["表备注"] + "\" isLeaf=\"false\" xmi.id=\"{" + Guid.NewGuid() + "}\" isAbstract=\"false\" visibility=\"public\">");
                            sb.AppendLine("                        <UML:ModelElement.taggedValue>");
                            sb.AppendLine("     <UML:TaggedValue tag=\"new\" value=\"false\"/>");
                            sb.AppendLine("   <UML:TaggedValue tag=\"unsafe\" value=\"false\"/>");
                            sb.AppendLine("  </UML:ModelElement.taggedValue>");
                            sb.AppendLine("  <UML:Classifier.feature>");
                        }
                    }

                    //每个表的中间内容
                    if (chkIgnoreNull.Checked)
                    {
                        if (dr["备注"] != DBNull.Value)
                        //继续执行
                        {
                            if (dr["备注"].ToString().Trim() != "")
                                sb.AppendLine("|" + dr["字段名"] + "|" + dr["类型"] + "(" + dr["长度"] + ")" + "|" + dr["主键"] + "|" + dr["标识"] + "|" + dr["允许空"] + "|" + FilterText(dr["默认值"]) + "|" + FilterText(dr) + "|");
                        }
                    }
                    else if (!chkXML.Checked)
                        sb.AppendLine("|" + dr["字段名"] + "|" + dr["类型"] + "(" + dr["长度"] + ")" + "|" + dr["主键"] + "|" + dr["标识"] + "|" + dr["允许空"] + "|" + FilterText(dr["默认值"]) + "|" + FilterText(dr) + "|");
                    else
                    {
                        sb.AppendLine("                                <UML:Attribute name=\"" + dr["字段名"] + "\" xmi.id=\"{" + Guid.NewGuid() + "}\" ownerScope=\"instance\" visibility=\"private\" changeability=\"changeable\"> ");
                        sb.AppendLine("                                <UML:Attribute.initialValue> ");
                        sb.AppendLine("                                    <UML:Expression body=\"" + FilterText(dr) + "\" xmi.id=\"Expr" + i + "\"/> ");
                        sb.AppendLine("                                </UML:Attribute.initialValue> ");
                        sb.AppendLine("                                <UML:StructuralFeature.type> ");
                        sb.AppendLine("                                    <UML:Classifier xmi.idref=\"Dttp0\"/> ");
                        sb.AppendLine("                                </UML:StructuralFeature.type> ");
                        sb.AppendLine("                            </UML:Attribute>");
                    }
                    //每个表的尾部内容
                    //var currentTableName = dr["表名"].ToString();
                    //if (currentTableName != lastTableName)
                    //{
                    //    sb.AppendLine(" </UML:Classifier.feature>");
                    //    sb.AppendLine("</UML:Class>");
                    //}
                    lastTableName = dr["表名"].ToString();
                    i++;
                }


                if (chkXML.Checked)
                {
                    sb.AppendLine(" </UML:Classifier.feature>");
                    sb.AppendLine("</UML:Class>");
                    sb.AppendLine(Properties.Resources.bottomtext);
                }


                var fileName = "导出.md";
                if (chkXML.Checked)
                    fileName = "导出.xml";

                using (System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "\\" + fileName, System.IO.FileMode.Create,
                        System.IO.FileAccess.ReadWrite))
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine(sb.ToString());
                    }
                }
                sb.Length = 0;

            }
            catch (Exception ex)
            {
                CSHelper.saveErrLog(ex.Message + "\n" + ex.StackTrace, DateTime.Now.ToString("yyyy-MM-dd") + "-ex");
            }
            finally
            {
                //完成后自动打开生成的脚本文件夹
                if (Directory.Exists(Application.StartupPath + "\\") != false)
                {
                    string path = Application.StartupPath + "\\"; System.Diagnostics.Process.Start("explorer.exe", path);
                }
                //隐藏加载中图片
                ShowLoading(false);
            }
        }
        //过滤数据库一些默认值
        //默认值影响到生成文档的格式故过滤
        public string FilterText(object o)
        {
            string text = o.ToString();
            if (text.Contains("\n"))
            {
                text = text.Replace("\n", "");
            }
            if (text.Contains("*"))
            {
                text = text.Replace("*", "");
            }
            if (text.Contains("/"))
            {
                text = text.Replace("/", "");
            }
            if (text.Length > 10)
            {
                text = text.Remove(10, text.Length - 10) + "已省略";
            }
            return text;
        }
        //过滤一些常用的字段
        public string FilterText(SqlDataReader dr)
        {
            var remark = "";
            switch (dr["字段名"].ToString())
            {
                case "fcreatedate": remark = "创建日期"; break;
                case "fmodifydate": remark = "编辑日期"; break;
                case "fUserNo": remark = "新增这条记录的操作人员编号"; break;
                case "n_last": remark = "自动编号，最后编码值"; break;
                case "ifdown": remark = "是否下载同步了本条数据"; break;
                case "Fpcno": remark = "本机站点编号"; break;
                case "FClub": remark = "分公司编号"; break;
                case "fEntityNo": remark = "总公司编号"; break;
                default: remark = dr["备注"].ToString(); break;
            }
            return remark;
        }
        public void showDataGirdView(int selectRow)
        {
            try
            {
                string sql = "";
                sql += @"SELECT   d.name
                表名 ,f.remark 表备注,
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
        ISNULL(e.text, '') 默认值,
        CASE WHEN (SELECT g.remark FROM t_table_field g WHERE g.table_name=d.NAME AND g.table_field=a.name) IS NOT NULL THEN (SELECT g.remark FROM t_table_field g WHERE g.table_name=d.NAME AND g.table_field=a.name) ELSE (SELECT pbc_cmnt FROM dbo.pbcatcol WHERE pbc_tnam=d.name AND pbc_cnam=a.name) END 备注
FROM    syscolumns a
        LEFT JOIN systypes b ON a.xtype = b.xusertype
        INNER JOIN sysobjects d ON a.id = d.id
                                   AND d.xtype = 'U'
                                   AND d.name <> 'dtproperties'
        LEFT JOIN syscomments e ON a.cdefault = e.id 
        LEFT JOIN t_table_name f ON d.NAME=f.table_name";
                if (chkAccurate.Checked == false)
                    sql += " WHERE d.name like '%" + txtTableName.Text.Trim() + "%'";
                else
                    sql += " WHERE d.name = '" + txtTableName.Text.Trim() + "'";
                sql += @"ORDER BY a.id ,a.colorder";
                SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
                conn.Open();
                SqlDataAdapter huoche = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                huoche.Fill(ds, "table");
                dgvTableAttribute.DataSource = ds.Tables["table"];
                if (selectRow != 0)
                {
                    dgvTableAttribute.Rows[selectRow].Selected = true;
                    dgvTableAttribute.FirstDisplayedScrollingRowIndex = selectRow;
                }
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "显示列表出错啦");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           showDataGirdView(0);
           CSHelper.WriteINI("LastWord", txtTableName.Text);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTableAttribute.RowCount > 1)
            {
                if (dgvTableAttribute.CurrentRow.Cells[1].Value != null)
                {
                    label4.Text = dgvTableAttribute.CurrentRow.Cells[1].Value.ToString();
                    textBox2.Text = dgvTableAttribute.CurrentRow.Cells["表备注"].Value.ToString();
                }
            }
        }

        //保存修改
        private void button3_Click(object sender, EventArgs e)
        {
            int lastRow = 0;
            //改表名备注
            if (dgvTableAttribute.Rows.Count > 1 && textBox2.Text.Trim() != string.Empty && textBox2.Text.Trim() != dgvTableAttribute.CurrentRow.Cells["表备注"].Value.ToString())
            {
                if (CSHelper.ifExist("select count(*) from t_table_name where table_name='" + label4.Text + "'") > 0)
                {
                    //修改表名备注
                    CSHelper.exec_sql("update t_table_name set remark ='" + textBox2.Text.Trim() + "' where table_name='" + label4.Text + "'");
                }
                else
                {
                    //新增表名备注
                    CSHelper.exec_sql("insert into t_table_name values('" + label4.Text + "','" + textBox2.Text.Trim() + "')");
                }
                //MessageBox.Show("表备注更新成功！");
            }


            //改表字段名重新备注

            //检索修改备注列具有有效值的字段名和备注
            List<string> updateList = new List<string>();
            int row = dgvTableAttribute.Rows.Count;
            for (int i = 0; i < row; i++)
            {
                //MessageBox.Show(dataGridView1.Rows[i].Cells["Column13"].Value.ToString());
                if (dgvTableAttribute.Rows[i].Cells["Column13"].Value != null)
                {
                    if (dgvTableAttribute.Rows[i].Cells["Column13"].Value.ToString().Trim() != "")
                        updateList.Add(dgvTableAttribute.Rows[i].Cells["Column1"].Value.ToString() + "," + dgvTableAttribute.Rows[i].Cells["Column3"].Value.ToString() + "," + dgvTableAttribute.Rows[i].Cells["Column13"].Value.ToString());
                    lastRow = i;
                }
            }

            string a = "";
            for (int i = 0; i < updateList.Count; i++)
            {
                a += "\n" + updateList[i].ToString();
            }

            if (updateList.Count == 0)
            {
                //do nothing！
            }
            else
            {
                //MessageBox.Show(a);

                foreach (string item in updateList)
                {
                    string[] arr = item.Split(',');

                    //判断是否已经存在
                    if (CSHelper.ifExist("select count(*) from t_table_field where table_name='" + label4.Text + "' and table_field='" + arr[1] + "' ") > 0)
                    {
                        //存在则修改
                        CSHelper.exec_sql("update t_table_field set remark ='" + arr[2] + "' where table_name='" + label4.Text + "' and table_field='" + arr[1] + "' ");
                    }
                    else
                    {
                        //不存在就新增
                        CSHelper.exec_sql("insert into t_table_field values('" + arr[0] + "','" + arr[1] + "','" + arr[2] + "')");
                    }

                }
                //MessageBox.Show("表字段备注更新成功！");  
            }

            //刷新
            showDataGirdView(lastRow);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //耗时操作在后台进程进行
            Thread worker = new Thread(delegate()
              {
                  bulidMarkdown();
              });
            worker.IsBackground = true;
            worker.Start();
        }

        //双击复制
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTableAttribute.CurrentCell.Value != null)
            {
                Clipboard.SetDataObject(dgvTableAttribute.CurrentCell.Value.ToString());
            }
        }

        public int RowCount = 0;
        public int SetGetRow
        {
            set
            {
                if (RowCount != value) { RowCount = value; }
            }
            get { return dgvTableAttribute.CurrentRow.Index; }
        }

        //查询功能
        private void button4_Click(object sender, EventArgs e)
        {
        start:
            if (txtField.Text.Trim() == "") return;
            int row = dgvTableAttribute.Rows.Count;//得到总行数 
            int cell = dgvTableAttribute.Rows[1].Cells.Count;//得到总列数

            for (int i = RowCount; i < row; i++)//得到总行数并在之内循环 
            {
                for (int j = 0; j < cell; j++)//得到总列数并在之内循环 
                {
                    //精确查找定位
                    if (dgvTableAttribute.Rows[i].Cells[j].Value != null)
                    {
                        if (dgvTableAttribute.Rows[i].Cells[j].Value.ToString().Trim().Contains(txtField.Text.Trim()))
                        {
                            //对比TexBox中的值是否与dataGridView中的值相同（上面这句）
                            dgvTableAttribute.CurrentCell = dgvTableAttribute[j, i];//定位到相同的单元格 
                            dgvTableAttribute.Rows[i].Selected = true;//定位到行 
                            SetGetRow = i + 1;
                            return;//返回
                        }
                    }

                }
            }

            DialogResult dr = MessageBox.Show("已经到尾了!是否从头检索？", "友情提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                dgvTableAttribute.Rows[0].Selected = true;
                RowCount = 0;
                goto start;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //记录上次查询的
            CSHelper.WriteINI("LastWord", txtTableName.Text);
        }


        //快捷键
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //保存修改
            if (e.Control && e.KeyCode == Keys.S)
            {
                //MessageBox.Show("保存成功！");
                button3_Click(this, EventArgs.Empty);
            }
            //查找字段
            if (e.Control && e.KeyCode == Keys.F)
            {
                //MessageBox.Show("保存成功！");
                txtField.Focus();
                txtField.SelectAll();
            }
            //输出
            if (e.Control && e.KeyCode == Keys.P)
            {
                //MessageBox.Show("保存成功！");
                button2_Click(this, EventArgs.Empty);
            }
            //查询表刷新表
            if (e.KeyCode == Keys.F5)
            {
                //MessageBox.Show("保存成功！");
                button1_Click(this, EventArgs.Empty);
            }
        }



    }
}
