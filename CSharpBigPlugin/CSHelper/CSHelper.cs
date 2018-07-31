using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data;
public static class CSHelper
{
    #region //保存text
    public static void saveTextFile(string log, string txtName, string expandedName,bool isAppend=false)
    {
        lock (locker)
        {
            //log = DateTime.Now.ToString() + " : " + log;
            string path = Application.StartupPath + "\\";
            if (isAppend == true && File.Exists(path + @"\" + txtName + "." + expandedName))
            {
                StreamWriter SW;
                SW = File.AppendText(path + @"\" + txtName + "." + expandedName);
                SW.WriteLine(log);
                SW.Close();
            }
            else
            {
                StreamWriter SW;
                SW = File.CreateText(path + @"\" + txtName + "." + expandedName);
                SW.WriteLine(log);
                SW.Close();
            }
        }
    }
    #endregion

    //加一把锁
    public static object locker = new object();
    #region //保存log
    public static void saveErrLog(string log, string txtName)
    {
        lock (locker)
        {
            //txtName = "log";
            log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + " : " + log;
            string path = Application.StartupPath + "\\";
            if (File.Exists(path + @"\" + txtName + ".txt"))
            {
                StreamWriter SW;
                SW = File.AppendText(path + @"\" + txtName + ".txt");
                SW.WriteLine(log + "\r\n");
                SW.Close();
            }
            else
            {
                StreamWriter SW;
                SW = File.CreateText(path + @"\" + txtName + ".txt");
                SW.WriteLine(log + "\r\n");
                SW.Close();
            }
        }
    }
	#endregion

	#region //保存log
	public static void saveFile(string log, string txtName,string extension)
	{
		//txtName = "log";
		//log = DateTime.Now.ToString() + " : " + log;
		string path = Application.StartupPath + "\\XrmSnippets\\";
		//if (File.Exists(path + @"\" + txtName + "."+ extension))
		//{
		//	StreamWriter SW;
		//	SW = File.AppendText(path + @"\" + txtName + "."+ extension);
		//	SW.WriteLine(log);//SW.WriteLine(log + "\r\n");
		//	SW.Close();
		//}
		//else
		//{
			StreamWriter SW;
			SW = File.CreateText(path + @"\" + txtName + "."+ extension);
			SW.WriteLine(log);//SW.WriteLine(log + "\r\n");
			SW.Close();
		//}

	}
	#endregion

	#region "声明变量"

	/// <summary>
	/// 写入INI文件
	/// </summary>
	/// <param name="section">节点名称[如[TypeName]]</param>
	/// <param name="key">键</param>
	/// <param name="val">值</param>
	/// <param name="filepath">文件路径</param>
	/// <returns></returns>
	[System.Runtime.InteropServices.DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
    /// <summary>
    /// 读取INI文件
    /// </summary>
    /// <param name="section">节点名称</param>
    /// <param name="key">键</param>
    /// <param name="def">值</param>
    /// <param name="retval">stringbulider对象</param>
    /// <param name="size">字节大小</param>
    /// <param name="filePath">文件路径</param>
    /// <returns></returns>
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

    private static string strFilePath = Application.StartupPath + "\\lxerp.ini";//获取INI文件路径
    private static string strSec = ""; //INI文件名

    #endregion

    #region "自定义读取INI文件中的内容方法"
    /// <summary>
    /// 自定义读取INI文件中的内容方法
    /// </summary>
    /// <param name="Section">键</param>
    /// <param name="key">值</param>
    /// <returns></returns>
    public static string ReadINI(string Section, string key)
    {
        StringBuilder temp = new StringBuilder(1024);
        GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
        return temp.ToString();
    }

    public static long WriteINI(string key, string value)
    {
        long result = 0;
        result = WritePrivateProfileString("Connection", key, value, strFilePath);
        return result;
    }

    public static string ReadINI(string Section, string key, string path)
    {

        StringBuilder temp = new StringBuilder(1024);
        GetPrivateProfileString(Section, key, "", temp, 1024, path);
        return temp.ToString();
    }
    public static long WriteINI(string key, string value, string path)
    {
        long result = 0;
        result = WritePrivateProfileString("Connection", key, value, path);
        return result;
    }
    #endregion

    #region "获取sqlconn连接参数"

    public static string sqlconn;

    public static void getsqlconn()
    {
        try
        {
            if (File.Exists(strFilePath))//读取时先要判读INI文件是否存在
            {

                //strSec = Path.GetFileNameWithoutExtension(strFilePath);
                strSec = "Connection";
                string Database = ReadINI(strSec, "Database");
                string ServerName = ReadINI(strSec, "ServerName");
                string LogId = ReadINI(strSec, "LogId");
                string Logpass = ReadINI(strSec, "Logpass");
                sqlconn = @"server=" + ServerName + ";database = " + Database + "; uid =" + LogId + "; pwd = " + Logpass + ";";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion

    #region //根据传入的sql语句返回受影响的行数
    /// <summary>
    /// 根据传入的sql语句返回受影响的行数
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static int exec_sql(string sql)
    {
        int result = 0;
        SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
        try
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.CommandTimeout = 60*10;//10分钟
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
    public static int ifExist(string sql)
    {
        int result = 0;
        SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
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
    public static string ExecuteScalar(string sql)
    {
        string result = string.Empty;
        SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
        try
        {

            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            var obj=cmd.ExecuteScalar();
            if(obj==DBNull.Value)
            {
                return result;
            }
            else if (obj == null)
            {
                return result;
            }
            result = obj.ToString();

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

    #region 返回List<Dictionary<string,object>>
    /// <summary>
    /// 返回List<Dictionary<string,object>>
    /// </summary>
    /// <param name="sql">查询sql</param>
    /// <returns>List<Dictionary<string,object>></returns>
    public static List<Dictionary<string,object>> GetListBySql(string sql)
    {
       
        SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
        var list=new List<Dictionary<string,object>>();
       
        try
        {

            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            var dr= cmd.ExecuteReader();
            while (dr.Read())
            {
                var dict = new Dictionary<string, object>();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    
                    dict.Add(dr.GetName(i), dr.GetValue(i));
                }
                list.Add(dict);
            }
            return list;
        }
        catch (Exception ex)
        {
            CSHelper.saveErrLog(sql + ex.Message, DateTime.Now.ToString("yyyy-MM-dd") + "-sql_err");
            return null;
        }

        finally
        {
            conn.Close();
            conn.Dispose();
        }

       
    }
    #endregion


    #region 返回第一列第一行
    /// <summary>
    /// 返回第一行
    /// </summary>
    /// <param name="sql">查询sql</param>
    /// <returns>字符串</returns>
    public static Dictionary<string,object> GetDictBySql(string sql)
    {
        var ReturnDict = new Dictionary<string, object>();

        SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
        try
        {

            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    ReturnDict.Add(dr.GetName(i), dr.GetValue(i));
                }
            }

        }
        catch (Exception ex)
        {
            CSHelper.saveErrLog(sql + ex.Message, DateTime.Now.ToString("yyyy-MM-dd") + "-sql_err");
            return null;
        }

        finally
        {
            conn.Close();
            conn.Dispose();
        }

        return ReturnDict;
    }
    #endregion

    #region //明华IC读写器

    public static int icdev; // 通讯设备标识符

    [DllImport("mwrf32.dll", EntryPoint = "rf_init", SetLastError = true,
           CharSet = CharSet.Auto, ExactSpelling = false,
           CallingConvention = CallingConvention.StdCall)]
    public static extern int rf_init(Int16 port, int baud);

    [DllImport("mwrf32.dll", EntryPoint = "rf_exit", SetLastError = true,
           CharSet = CharSet.Auto, ExactSpelling = false,
           CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_exit(int icdev);

    [DllImport("mwrf32.dll", EntryPoint = "rf_beep", SetLastError = true,
           CharSet = CharSet.Auto, ExactSpelling = false,
           CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_beep(int icdev, int msec);

    [DllImport("mwrf32.dll", EntryPoint = "rf_get_status", SetLastError = true,
          CharSet = CharSet.Auto, ExactSpelling = false,
          CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_get_status(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] state);

    [DllImport("mwrf32.dll", EntryPoint = "rf_load_key", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_load_key(int icdev, int mode, int secnr, [MarshalAs(UnmanagedType.LPArray)]byte[] keybuff);

    [DllImport("mwrf32.dll", EntryPoint = "rf_load_key_hex", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_load_key_hex(int icdev, int mode, int secnr, [MarshalAs(UnmanagedType.LPArray)]byte[] keybuff);


    [DllImport("mwrf32.dll", EntryPoint = "a_hex", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 a_hex([MarshalAs(UnmanagedType.LPArray)]byte[] asc, [MarshalAs(UnmanagedType.LPArray)]byte[] hex, int len);

    [DllImport("mwrf32.dll", EntryPoint = "hex_a", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 hex_a([MarshalAs(UnmanagedType.LPArray)]byte[] hex, [MarshalAs(UnmanagedType.LPArray)]byte[] asc, int len);

    [DllImport("mwrf32.dll", EntryPoint = "rf_reset", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_reset(int icdev, int msec);

    [DllImport("mwrf32.dll", EntryPoint = "rf_request", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_request(int icdev, int mode, out UInt16 tagtype);


    [DllImport("mwrf32.dll", EntryPoint = "rf_anticoll", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_anticoll(int icdev, int bcnt, out uint snr);

    [DllImport("mwrf32.dll", EntryPoint = "rf_select", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_select(int icdev, uint snr, out byte size);

    [DllImport("mwrf32.dll", EntryPoint = "rf_card", SetLastError = true,
        CharSet = CharSet.Auto, ExactSpelling = false,
        CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_card(int icdev, int mode, [MarshalAs(UnmanagedType.LPArray)]byte[] snr);

    [DllImport("mwrf32.dll", EntryPoint = "rf_authentication", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_authentication(int icdev, int mode, int secnr);

    [DllImport("mwrf32.dll", EntryPoint = "rf_authentication_2", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_authentication_2(int icdev, int mode, int keynr, int blocknr);

    [DllImport("mwrf32.dll", EntryPoint = "rf_read", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_read(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

    [DllImport("mwrf32.dll", EntryPoint = "rf_read_hex", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_read_hex(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

    [DllImport("mwrf32.dll", EntryPoint = "rf_write_hex", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_write_hex(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

    [DllImport("mwrf32.dll", EntryPoint = "rf_write", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_write(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

    [DllImport("mwrf32.dll", EntryPoint = "rf_halt", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_halt(int icdev);

    [DllImport("mwrf32.dll", EntryPoint = "rf_changeb3", SetLastError = true,
        CharSet = CharSet.Auto, ExactSpelling = false,
        CallingConvention = CallingConvention.StdCall)]
    public static extern Int16 rf_changeb3(int icdev, int sector, [MarshalAs(UnmanagedType.LPArray)]byte[] keya, int B0, int B1,
          int B2, int B3, int Bk, [MarshalAs(UnmanagedType.LPArray)]byte[] keyb);
    #endregion

<<<<<<< HEAD
    #region 根据传入的查询sql返回字典的结果
    public static Dictionary<string, object> GetDictBySql(string sql)
    {
        Dictionary<string, object> ReturnDict = new Dictionary<string, object>();
        SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
        Dictionary<string, object> result;
        try
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    ReturnDict.Add(dr.GetName(i), dr.GetValue(i));
                }
            }
        }
        catch (Exception ex)
        {
            CSHelper.saveErrLog(sql + ex.Message, DateTime.Now.ToString("yyyy-MM-dd") + "-sql_err");
            result = null;
            return result;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        result = ReturnDict;
        return result;
    } 
    #endregion

    #region 根据传入的sql返回列表
    public static List<Dictionary<string, object>> GetListBySql(string sql)
    {
        SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        List<Dictionary<string, object>> result;
        try
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    dict.Add(dr.GetName(i), dr.GetValue(i));
                }
                list.Add(dict);
            }
            result = list;
        }
        catch (Exception ex)
        {
            CSHelper.saveErrLog(sql + ex.Message, DateTime.Now.ToString("yyyy-MM-dd") + "-sql_err");
            result = null;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return result;
    }
    
    #endregion

    #region 获取数据库连接状态
    public static bool getConnectState()
    {
        SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
        bool result;
        try
        {
            conn.Open();
            result = true;
        }
        catch (Exception ex)
        {
            CSHelper.saveErrLog(ex.Message, DateTime.Now.ToString("yyyy-MM-dd") + "-sql_err");
            result = false;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return result;
    } 
    #endregion

    #region 获取datatable
    public static DataTable GetTable(string strSQL, SqlParameter[] pas, CommandType cmdtype)
    {
        DataTable dt = new DataTable();
=======
    #region 执行查询，返回DataTable对象-----------------------

    public static DataTable GetTable(string strSQL)
    {
        return GetTable(strSQL, null);
    }
    public static DataTable GetTable(string strSQL, SqlParameter[] pas)
    {
        return GetTable(strSQL, pas, CommandType.Text);
    }
    /// <summary>
    /// 执行查询，返回DataTable对象
    /// </summary>
    /// <param name="strSQL">sql语句</param>
    /// <param name="pas">参数数组</param>
    /// <param name="cmdtype">Command类型</param>
    /// <returns>DataTable对象</returns>
    public static DataTable GetTable(string strSQL, SqlParameter[] pas, CommandType cmdtype)
    {
        DataTable dt = new DataTable(); ;
>>>>>>> c208933cd1cc9494d11bf10dbe066cdb346bb359
        using (SqlConnection conn = new SqlConnection(CSHelper.sqlconn))
        {
            SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
            da.SelectCommand.CommandType = cmdtype;
            if (pas != null)
            {
                da.SelectCommand.Parameters.AddRange(pas);
            }
            da.Fill(dt);
        }
        return dt;
    }
<<<<<<< HEAD
    public static DataTable GetTable(string strSQL, SqlParameter[] pas)
    {
        return CSHelper.GetTable(strSQL, pas, CommandType.Text);
    }
    public static DataTable GetTable(string strSQL)
    {
        return CSHelper.GetTable(strSQL, null);
    } 
    #endregion
=======
>>>>>>> c208933cd1cc9494d11bf10dbe066cdb346bb359



    #endregion


    #region //判断是否可以连接数据库
    /// <summary>
    /// 判断是否可以连接数据库
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static bool getConnectState()
    {
        bool isConnected = false;
        SqlConnection conn = new SqlConnection(CSHelper.sqlconn);
        try
        {
            //conn.ConnectionTimeout = 1000;
            conn.Open();
            return true;
           
        }
        catch (Exception ex)
        {

            CSHelper.saveErrLog(ex.Message, DateTime.Now.ToString("yyyy-MM-dd") + "-sql_err");
            return false;
        }

        finally
        {
            conn.Close();
            conn.Dispose();
        }

        return isConnected;
    }
    #endregion


    #region //使用事务执行多条SQL(插入、更新、删除)
    /// <summary>
    /// 使用事务执行多条SQL(插入、更新、删除)
    /// </summary>
    /// <param name="sqls"></param>
    public static void Exec_Tansaction(List<string> sqls)
    {
        var sqlsb = new StringBuilder();;
        if (sqls.Count == 0) return;

        SqlConnection conn = new SqlConnection(sqlconn);
        conn.Open();
        // 启动一个事务。 
        SqlTransaction myTran = conn.BeginTransaction();
        // 为事务创建一个命令
        SqlCommand myCom = new SqlCommand();
        myCom.Connection = conn;
        myCom.Transaction = myTran;
        try
        {
            foreach (string sql in sqls)
            {
                sqlsb.AppendLine(sql);
                myCom.CommandText = sql;
                myCom.ExecuteNonQuery();
                
            }
            myTran.Commit();//提交事务
        }
        catch (Exception ex)
        {
            myTran.Rollback();

            CSHelper.saveErrLog("提交数据失败" + ex.Message + "\n\r"+sqlsb.ToString(), DateTime.Now.ToString("yyyy-MM-dd") + "-sql_err");
            //返回异常的错误信息 
            //MessageBox.Show("提交数据失败!\n" + Ex.ToString(), "异常信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            throw new Exception("提交数据失败!\n" + ex.ToString());
        }
        finally
        {
            conn.Close();
        }
    }
    #endregion

    #region 指静脉 XGComApi
    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_ConnectDev(String sDev, String sPassword);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_CloseDev(int lDevHandle);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_GetDevParam(int lDevHandle, byte[] bParam);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_SetDevParam(int lDevHandle, String sParam);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_PlayDevSound(int lDevHandle, int Sound);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_CheckFinger(int lDevHandle);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_GetVeinChara(int lDevHandle, byte[] bChara, int lTimeout);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_ReadDevTemp(int lDevHandle, int lUserId, byte[] sTemp);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_WriteDevTemp(int lDevHandle, int lUserId, String sTemp, String sUserInfo);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_DeleteDevTemp(int lDevHandle, int lUserId);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_GetDevDebugInfo(int lDevHandle, String sFileName);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_Upgrade(int lDevHandle, String sFileName);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_GetImgFormData(String sData, int lDataLen, byte[] sImg);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_GetImgFormDev(int lDevHandle, byte[] sImg, int lTimeout);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_GetCharaFromImg(String sImg, byte[] sChara);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_ReadDevLog(int lDevHandle, String sStartTime, String sEndTime, byte[] sLog);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_WriteDevUserInfo(int lDevHandle, int lStartID, int lNum, String sUserInfo);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_ReadDevUserInfo(int lDevHandle, int lStartID, int lNum, byte[] sUserInfo);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_SendCmdPacket(int lDevHandle, int lCmd, byte[] sData);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_RecvCmdPacket(int lDevHandle, byte[] bData, int lTimeout);

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //以下是算法库相关API

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_CharaMatch(String sChara1, String sChara2, int Score);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_CreateVeinTemp(String sChara1, String sChara2, String sChara3, byte[] bTemp, String sUserInfo);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_CreateVeinLib(int lUserNum);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_DestroyVeinLib(int lLibHandle);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_ImportVeinTemp(int lLibHandle, int lUserId, String sTemp, String sUserInfo);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_ExportVeinTemp(int lLibHandle, int lUserId, byte[] bTemp);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_CleanVeinTemp(int lLibHandle, int lUserId);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_SearchUser(int lLibHandle, String sChara, int lTh, byte[] bUserInfo);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_VerifyUser(String sTemp, String sChara, int Score, byte[] bStudyTemp, byte[] bUserInfo);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_SetUserInfo(int lLibHandle, int lUserId, String sUserInfo);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_GetUserInfo(int lLibHandle, int lUserId, byte[] sUserInfo);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_GetTempUserInfo(String sTemp, byte[] sUserInfo);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_NetPackParse(byte[] bNetBuf, int lBufSize, byte[] sCmd, byte[] sSN, byte[] sData);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_AsciiToHex(byte[] sAscii, byte[] bHex);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_HexToAscii(byte[] bHex, int lByte, byte[] sAscii);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_SocketServerInit(int lPort, String sParam);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_SocketAccept(int lSocket, byte[] sInfo);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_SocketRecvPack(int lSocket, byte[] sCMD, byte[] sSN, byte[] sData);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_SocketSendPack(int lSocket, int lCmd, String sData);

    [DllImport("XGComApi.dll", CharSet = CharSet.Ansi)]
    public static extern int FV_SocketClose(int lSocket);

    [DllImport("winmm.dll")]
    public static extern bool PlaySound(String Filename, int Mod, int Flags);

    [DllImport("kernel32.dll")]
    public static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    public static extern bool FreeConsole();
    #endregion
}

