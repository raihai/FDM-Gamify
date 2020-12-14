using System.Data;
using System.Data.SqlClient;

namespace fdm_gamify2
{
    public class DatabaseConnection
    {
        // connection variables
        private const string ConString = "Data Source=localhost;Initial Catalog=test;User id=root;Password=SqlPwd01";
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

        // shows data returned by query in a grid view
        public object ShowDataGridView(string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, ConString);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet.Tables[0];
        }
    }
}