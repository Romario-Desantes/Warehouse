using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    internal class DB
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=1234;database=warehousedb;");

        public void openConn()
        {
            if(conn.State == System.Data.ConnectionState.Closed) { 
                conn.Open();
            }
        }

        public void closeConn()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public MySqlConnection getConn() { return conn; }
 
    }
}
