﻿using System.Data;
using System.Data.SqlClient;

namespace fdm_gamify2
{
    public class DatabaseConnection
    {
        // connection variables
        // NOTE: ConString may not work for you guys, you can copy it and change user details to your machine
        // just comment out whichever one(s) aren't for your machine
        private const string ConString = "Data Source=localhost;Initial Catalog=csc2033;User id=root;Password=SqlPwd01";
        private SqlConnection _connection;

        // opens connection to database
        public void OpenConnection()
        {
            _connection = new SqlConnection(ConString);
            _connection.Open();
        }

        // close connection to database
        public void CloseConnection()
        {
            _connection.Close();
        }

        // executes a given query
        public void ExecuteQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        // reads forward-only stream of rows database
        public SqlDataReader DataReader(string query)
        {
            SqlCommand cmd = new SqlCommand(query, _connection);
            return cmd.ExecuteReader();
        }

        // returns query data as a data table (for leaderboard)
        public DataTable GetDataTable(string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, ConString);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }
}