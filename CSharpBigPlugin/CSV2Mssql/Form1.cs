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

namespace CSV2Mssql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] path = null;
        public long linecount = 0;

        /// <summary>
        /// 更新lable显示的文本
        /// </summary>
        /// <param name="ifshow"></param>
        public void UpdateLableText(Label lable, string text)
        {
            //无参数,但是返回值为bool类型
            this.Invoke(new Func<bool>(delegate()
            {
                lable.Text = text;
                return true; //返回值
            }));
        }
        public void UpdateTextBoxText(TextBox textbox, string text)
        {
            //无参数,但是返回值为bool类型
            this.Invoke(new Func<bool>(delegate()
            {
                textbox.Text = text;
                return true; //返回值
            }));
        }

        private void btnTestConn_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(textBoxConnectionString.Text);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    MessageBox.Show("测试连接成功！");
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        private void btrCreateCSV_Click(object sender, EventArgs e)
        {
            Thread worker = new Thread(delegate()
            {
                CreateCSV();
                //完成后自动打开生成的脚本文件夹
                if (Directory.Exists(Application.StartupPath ) != false)
                {
                    string path = Application.StartupPath ; System.Diagnostics.Process.Start("explorer.exe", path);
                }
            });
            worker.IsBackground = true;
            worker.Start();
           
        }

        private void CreateCSV()
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
            const int group = 1000;
            int total = 0;
            bool containsTitle = true;
            try
            {

                conn.Open();
                string sql = txtSql.Text.Trim();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (containsTitle)
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            if (i == dr.FieldCount - 1)
                            {
                                sb.AppendFormat("{0}", dr.GetName(i).ToString());
                                containsTitle = false;
                                sb.Append("\n");
                            }
                            else
                            {
                                sb.AppendFormat("{0},", dr.GetName(i).ToString());
                            }
                        }
                       

                    }
                    for (int i = 0; i < dr.FieldCount; i++)
                    {

                        if (i == dr.FieldCount - 1)
                        {
                            sb.AppendFormat("{0}", dr.GetValue(i).ToString());
                        }
                        else
                        {
                            sb.AppendFormat("{0},", dr.GetValue(i).ToString());
                        }
                    }
                    sb.Append("\n");
                    total++;
                   UpdateLableText(lblTotal,"生成记录统计:" + total);
                    if (total % group == 0)
                    {
                        if (total == group)
                        {
                            CSHelper.saveTextFile(sb.ToString(), "导出", "csv");
                        }
                        else
                        {
                            CSHelper.saveTextFile(sb.ToString(), "导出", "csv", true);
                        }
                        sb.Length = 0;
                    }

                }
                 CSHelper.saveTextFile(sb.ToString(), "导出", "csv",true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CSHelper.getsqlconn();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        public int exist, sucess, err = 0;
        private void btnImport_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("请检查连接参数是否正确?\n\r" + textBoxConnectionString.Text + "\n\r正确点确定，否则取消", "友情提示", MessageBoxButtons.OKCancel);
            //if (result != DialogResult.OK)
            //{
            //    return;
            //}
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "打开";
                ofd.Filter = "所有文件|*.csv";
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
                    StringBuilder sbData = new StringBuilder();
                    linecount = 0;
                    for (int i = 0; i < path.Length; i++)
                    {
                        using (StreamReader sr = new StreamReader(path[i], System.Text.Encoding.GetEncoding("gb2312")))
                        {

                            string line = string.Empty;
                            while ((line = sr.ReadLine()) != null)
                            {
                                //如果读出一行不是空字符串
                                if (line.Trim() != "")
                                {
                                    //读出首行的字段名
                                    //if (linecount == 0)
                                    //{
                                    //    UpdateTextBoxText(txtCSVDemo, line);
                                    //}
                                    linecount++;
                                    if (linecount == 1 && cbIgnoreFirstLine.Checked)
                                    {
                                        //第一行遗弃
                                        continue;
                                    }
                                    switch (Insert(line))
                                    {
                                        //失败
                                        case 0: err++; break;
                                         //插入成功
                                        case 1: sucess++; break;
                                         //已经存在
                                        case 2: exist++; break;
                                        
                                    }
                                    sbData.Append(line + "\n");
                                    //UpdateLableText(labelCurrent, "csv记录统计:" + (linecount - 1));
                                    UpdateLableText(lblStatus, string.Format("导入状态:成功:{0}|失败:{1}|已存在:{2}|总:{3}", sucess, err, exist, (sucess + err + exist)));
                                }
                            }


                        }
                    }
                });
                worker.IsBackground = true;
                worker.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
            }
        }
        public int Insert(string data)
        {
          
            int result = 0;
            // [工号],[姓名],[开卡日期],[有效期至],[账号],[卡号],[部门编码]  
            //797004,新中新4,2016-10-9 0:00,2020-6-30 0:00,1,1371234346,1100102153
            try
            {
                var arr = data.Split(',');

                //先判断数据库是否存在
                if (ifExist(string.Format("select count(*) from mem_member where m_id='{0}'", arr[5])) > 0)
                {
                    result = 2;
                }
                else
                {
                    //如果不存在需要做的

                    //组成ETS的q_id
                    //更新序列号表的no
                    exec_sql("update set_serialno set no =no + 1 where code ='MEM' and fEntityNo ='LDTCO' and FClub ='99' ");
                    //分公司编号+1+set_serialno表的对应'mem'的no值，no值不足八位数以0填充
                    var no = ExecuteScalar("select no from set_serialno where code ='MEM' and fEntityNo ='LDTCO' and FClub ='99' ");
                    var q_id = "991" + no.PadLeft(8, '0');
                    //组成插入sql
                    string sql = string.Format(@"Insert mem_member ([m_id], [tickh], [mem_id], [m_id_type], [mem_iden], [card_ext], [namee], [namec], [nameelast], [q_id], [main_id], [first_mid], [cardhold], [if_hide], [sortorder], [familys], [nominee], [company_buy], [no_name], [order_text], [picture], [user_id], [user_time], [sex], [passport], [id], [birthday], [birth_zone], [titlec], [titlee], [nation], [memberkind], [status], [mem_period], [club_id], [pin_id], [certification], [data_input], [autho_indate], [autho_name], [joindate], [introdu_iden], [introducer], [introdu_memid], [sale_name], [issue_date], [if_closedate], [closedate], [status_date], [mem_password], [iccard_no], [issue_card], [autho_date], [autho_type], [autho_id], [autho_no], [check_no], [bank_name], [tel], [fax], [mobilphone], [address], [co_tel], [co_fax], [co_address], [co_poscode], [pobox], [certno], [mailto_label], [mailto], [mailto_magazine], [blood_type], [from_country], [from_zone], [from_city], [stay_country], [stay_zone], [stay_city], [maritus], [marrydate], [edu_grade], [hobby], [nickname], [spouse], [children], [e_mail], [postcode], [car], [language], [home_page], [park], [park_no], [native_place], [master_language], [race], [link_no], [co_namec], [co_namee], [co_departmemt], [co_position], [co_prof], [co_beeper], [co_mobil], [co_prop], [salary], [co_office], [incomes], [company_no], [found_date], [company_trade], [company_scale], [company_depute], [company_follows], [business_prop], [register_no], [billtocard], [rights], [credit_limit], [money_id], [co_enterfee], [co_security], [co_enterpaydate], [security], [co_enterendate], [co_periods], [co_paiedperiods], [co_paiedamt], [startmonthdate], [endmonthdate], [mfee_money], [monthfee], [mthyr], [renderway], [mfee_paiedamt], [call_times], [mem_paytype], [mem_owes], [open_bank], [open_office], [open_company], [open_billno], [open_credit], [open_creditno], [open_endate], [remark], [pos_tips], [pos_tips_type], [spare_no1], [spare_no2], [packet_no], [lease_endate], [hcap_refer], [hcap], [consum_amts], [consum_times], [user_content], [relative], [term], [beeper], [account_tips], [reminder], [zone], [city], [card_date], [spell], [co_countrycat], [co_postcode], [co_attn], [co_rep], [coupon], [title], [expiry_date], [renewed_date], [cancelled_date], [handledby], [application], [co_attn_2], [other_address], [other_postcode], [other_attn], [other_tel], [other_address2], [other_postcode2], [other_attn2], [other_tel2], [houseid], [housebank], [paytimelimit], [housetype], [housemoneytype], [housesalemoney], [buytime], [yearperiod], [posjustify], [wherefrom], [cohap], [trytimes], [ballbagnum], [othername], [tcredit_limit], [mcredit_limit], [dqcs], [jrcs], [source], [homeaddr], [hometel], [homepostcode], [modify], [modifytime], [contactperiod], [contactway], [linkman], [site], [linktime], [recommend], [introdusid], [cardsno], [cardbalance], [signertype], [couponno], [coupontype], [couponpay], [depositcard], [fEntityNo], [ifdown], [setautoid], [Web_UType], [FClub], [Fpcno], [specialflag], [fbillNo], [fhyinmpno], [ftickh], [statusinout], [fpowerdoor], [fifygmp], [factive], [fvisibleacc], [factivedate], [jecountall], [fyjje], [mem_password1], [cardbalancezs], [fgzjeall], [spcdxhje], [Fstatustype], [fnewhyje], [factdate], [fstudentno], [ifhyyjje])
                    Values('{0}','{0}',1,1,'4',null,null,'{2}',null,'{1}','{0}',null,1,0,null,null,null,null,null,null,null,null,'1900-01-01 0:00:00',null,null,null,null,null,null,null,null,4,0,0,'99',null,null,null,null,null,'{3}',null,null,null,null,'1900-01-01 0:00:00',null,'1900-12-31 0:00:00',null,null,null,0,null,null,null,null,null,null,'{0}',null,'{4}',null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,0.00,null,0.00,0.00,null,null,null,null,null,null,'1900-01-01 0:00:00','1900-01-01 0:00:00',null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,0,0,null,null,null,null,null,null,null,null,null,null,null,null,null,{0},0.00,null,null,null,null,1,'LDTCO','N   ',null,0,'99',null,null,null,null,null,0,0,0,0,0,null,0.00,0.00,null,null,0.00,null,0,null,null,null,0);"
                        , arr[5], q_id, arr[1],arr[2],arr[0]);
                    //执行sql

                    if (exec_sql(sql) > 0)
                    {
                        result = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace);
                result = 0;
            }
            return result;
        }

        #region //根据传入的sql语句返回受影响的行数
        /// <summary>
        /// 根据传入的sql语句返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public  int exec_sql(string sql)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(textBoxConnectionString.Text);
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.CommandTimeout = 60 * 10;//10分钟
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                CSHelper.saveErrLog(sql + ex.Message, DateTime.Now.ToString("yyyy-MM-dd") + "-sql_err");
                return result;
            }

            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return result;
        }
        #endregion

        #region //判断是否存在
        /// <summary>
        /// 判断存在
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public  int ifExist(string sql)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(textBoxConnectionString.Text);
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                result = int.Parse(cmd.ExecuteScalar().ToString());

            }
            catch (Exception ex)
            {
                CSHelper.saveErrLog(sql + ex.Message, DateTime.Now.ToString("yyyy-MM-dd") + "-sql_err");
                return result;
            }

            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return result;
        }
        #endregion

        #region 返回第一列第一行
        /// <summary>
        /// 返回第一列第一行
        /// </summary>
        /// <param name="sql">查询sql</param>
        /// <returns>字符串</returns>
        public  string ExecuteScalar(string sql)
        {
            string result = "";
            SqlConnection conn = new SqlConnection(textBoxConnectionString.Text);
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                result = cmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                CSHelper.saveErrLog(sql + ex.Message, DateTime.Now.ToString("yyyy-MM-dd") + "-sql_err");
                return result;
            }

            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return result;
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            var cardsno = ExecuteScalar("select cardsno from mem_member where m_id='" + txtM_id.Text.Trim() + "'");
            txtOldCardsno.Text = cardsno;
        }

        private void btnUpdateCardsno_Click(object sender, EventArgs e)
        {
            if (exec_sql(string.Format("update mem_member set cardsno='{0}' where m_id='{1}'", txtNewCardsno.Text.Trim(), txtM_id.Text.Trim())) > 0)
            {
                MessageBox.Show("更新成功！");
            }
            else
            {
                MessageBox.Show("更新失败！");
            }
        }
    }
}
