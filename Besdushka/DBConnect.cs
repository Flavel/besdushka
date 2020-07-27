using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Besdushka
{
    class DBConnect
    {
        private static string server = "localhost";
        private static string port = "3306";
        private static string username = "root";
        private static string password = "root";
        private static string database = "tasks";

        MySqlConnection connection = new MySqlConnection
            ($@"server ={server};port={port}; username={username};password={password};database={database}");


        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
