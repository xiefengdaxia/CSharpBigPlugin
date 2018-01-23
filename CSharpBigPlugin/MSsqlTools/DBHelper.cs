using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
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
        public static void getConn()
        {
            //connectionString = "server=" + Server + ";database=master;;uid=sa;pwd=1;";
            connectionString = "Data Source=" + Server + ";Initial Catalog=master;Integrated Security=SSPI;  ";
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
