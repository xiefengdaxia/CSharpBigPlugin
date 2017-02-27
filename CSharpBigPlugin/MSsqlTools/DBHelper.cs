using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace MSsqlTools
{
    public static class DBHelper
    {
        public static string connectionString = "server=.\\xsql2008;database=master;Trusted_Connection=SSPI";

        public static int execSql(string sql)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql,conn);
            result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }

        public static List<string> QuerySql(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<string> list = new List<string>();
                conn.Open();
                SqlCommand cmd= new SqlCommand(sql, conn);
                SqlDataReader dr= cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(dr[0].ToString());
                }
                return list;
            }

        }
}}
