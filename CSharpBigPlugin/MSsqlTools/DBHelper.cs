using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Security.Permissions;
namespace MSsqlTools
{
    public static class DBHelper
    {
        static DBHelper()
        {
            getConn();
        }
        public static string Server = @".\xsql2008";
        public static string connectionString;
        //[PrincipalPermission(SecurityAction.Demand, Role = @"BUILTIN\Administrators")]
        public static void getConn()
        {
           // WindowsImpersonationContext impersonationContext = identity.Impersonate();
            //connectionString = "server=" + Server + ";database=master;;uid=sa;pwd=1;";
            //connectionString = "Data Source=" + Server + ";Initial Catalog=master;Integrated Security=True;  ";
            connectionString = "Data Source=" + Server + ";Initial Catalog=master;Integrated Security=SSPI;  ";
            //connectionString = "Data Source=192.168.1.106,59157;Initial Catalog=master;Integrated Security=SSPI;  ";
            //IsAdministrator();
        }
        /// <summary>
        /// 确定当前主体是否属于具有指定 Administrator 的 Windows 用户组
        /// </summary>
        /// <returns>如果当前主体是指定的 Administrator 用户组的成员，则为 true；否则为 false。</returns>
        public static string IsAdministrator()
        {
            bool result;
            string user="";
            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                result = principal.IsInRole(WindowsBuiltInRole.Administrator);
                user=principal.Identity.Name;
                //http://www.cnblogs.com/Interkey/p/RunAsAdmin.html
                //AppDomain domain = Thread.GetDomain();
                //domain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                //WindowsPrincipal windowsPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;
                //result = windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                result = false;
            }
            return user;
        }
        public static int execSql(string sql)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            result = cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
            return result;
        }

        public static List<string> QuerySql(string sql)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            List<string> list = new List<string>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(dr[0].ToString());
            }
            conn.Close();
            conn.Dispose();
            return list;
        }
    }
}
