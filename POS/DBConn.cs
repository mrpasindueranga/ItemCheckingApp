using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace POS
{
    class DBConn
    {
        public SqlConnection conn = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();
        public SqlDataAdapter ada = new SqlDataAdapter();

        public DBConn()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
            else
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["POS"].ConnectionString;
                conn.Open();
                cmd.Connection = conn;
            }
        }
    }
}
