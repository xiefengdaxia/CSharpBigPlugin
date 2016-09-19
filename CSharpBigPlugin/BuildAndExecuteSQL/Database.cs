using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BuildAndExecuteSQL
{
    class Database : IDisposable
    {
        SqlConnection _Conn;
        SqlDataReader _DataReader;
        DataTable _SchemaTable;
        SqlCommand _Command;

        public void Connect(string connectString)
        {
            Close();
            _Conn = new SqlConnection(connectString);
            _Conn.Open();
        }

        public void Close()
        {
            Dispose();
        }

        public DataSet GetSchema(string queryString)
        {
            DataSet ds = new DataSet();


            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(
                queryString, _Conn);
            adapter.FillSchema(ds, SchemaType.Mapped);

            return ds;
        }

        public void OpenDataReader(string queryString)
        {
            _Command = new SqlCommand(queryString, _Conn);
            _SchemaTable = GetSchema(queryString).Tables[0];
            _DataReader = _Command.ExecuteReader();
        }


        public void CloseDataReader()
        {
            _DataReader.Close();
        }

        public void Clear()
        {
            _SchemaTable.Clear();
        }

        public DataRow GetNextRow()
        {
            if (_DataReader.Read())
            {
                DataRow row = _SchemaTable.NewRow();

                foreach (DataColumn col in _SchemaTable.Columns)
                {
                    row[col.ColumnName] = _DataReader[col.ColumnName];
                }

                return row;
            }
            else
            {
                return null;
            }
        }



        ~Database()
        {
            Close();
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (_Conn != null)
                {
                    _Command.Cancel();
                    _DataReader.Close();

                    if (_Conn.State != ConnectionState.Closed &&
                        _Conn.State != ConnectionState.Broken)
                    {
                        _Conn.Close();
                    }

                    _Conn = null;
                }
            }
            catch
            {
            }
        }

        #endregion
    }
}
