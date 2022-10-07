using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseDeDatos
{
    public abstract class DB
    {
        private string _connectionstring;
        protected SqlConnection _connection;

        public DB(string server, string db, bool trusted_connection)
        {
            _connectionstring = $"Data Source={server}; Initial Catalog={db}; Trusted_connection={trusted_connection}";
        }

        public void Connect()
        {
            // creación del objeto
            _connection = new SqlConnection(_connectionstring);
            _connection.Open();
        }

        public void Close()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
            
    }
}


