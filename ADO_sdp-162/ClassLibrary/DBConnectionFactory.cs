using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ClassLibrary
{
    internal class DBConnectionFactory
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString();
            }
        }

        public static SqlConnection GetConnection()
        {
            SqlConnection dbConnection = new SqlConnection(ConnectionString);
            return dbConnection;
        }
    }
}