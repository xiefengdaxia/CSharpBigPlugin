using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
public static class CSHelper
{
    #region //保存log
    public static void saveErrLog(string log, string txtName)
    {
        //txtName = "log";
        log = DateTime.Now.ToString() + " : " + log;
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


}

