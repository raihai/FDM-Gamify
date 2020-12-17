using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.SignalR;
using MySql.Data.MySqlClient;
using Renci.SshNet;
// uses mysql.data in NuGet
//uses Ssh.net install from https://github.com/sshnet/SSH.NET
namespace fdm_gamify2
{
    public class DatabaseConnection
    {

        // connection variables
        // when testing be sure to change connection string to suit your local database
        // perhaps create your own ConString and comment out whichever is not yours
        
        private const string ConString = "Data Source=cs-db.ncl.ac.uk;Initial Catalog=t2033t26;User id=t2033t26;Password=Sit-HewsRide";
        private MySqlConnection _connection;

        // opens connection to database
        public void OpenConnection()
        {
            Console.WriteLine("Start of open connection");
            SshClient client = SSHTunnel();
            Console.Write("reached end of opening connection");

        }

        // close connection to database
        public void CloseConnection()
        {
            _connection.Close();
        }

        // executes a given query
        public void ExecuteQuery(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        // reads forward-only stream of rows database
        public void DataReader(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, _connection); ;
            
        }

        // shows data returned by query in a grid view
        public DataTable GetDataTable(string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, ConString);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet.Tables[0];
        }

        /*
         *Creates SSH Tunnel to linux server
         * Replace Username and Passowrd in connection info variable to your own.
         * Once connected connects to shared db using a port from local host -> cs.db
         * Connects to SQL DB
         * 
         */
        public SshClient SSHTunnel()
        {
            string username = "";
            string password = "";
            Console.WriteLine("Start of method");
            var connectionInfo = new SshClient("cs-linux.ncl.ac.uk",
                username, password); 
            
            using (var client = connectionInfo)
            {
                client.Connect();
                if (client.IsConnected)
                {
                    Console.Write("hanging...");
                    //client.RunCommand("mysql -h cs-db.ncl.ac.uk -u t2033t26 -p Sit-HewsRide");
                    Console.Write("done");
                    var portForwarded = new ForwardedPortLocal("localhost", 3306, "cs-db.ncl.ac.uk", 3306);;
                    Console.WriteLine(portForwarded.IsStarted);
                    client.AddForwardedPort(portForwarded);
                    portForwarded.Start();
                    Console.Write(portForwarded.IsStarted);
                    MySqlConnection con = new MySqlConnection(
                            "SERVER= localhost;UID=t2033t26;PASSWORD=Sit-HewsRide;DATABASE=t2033t26");// server is equal to the first host variable in the port above ^^
                    con.Open();
                    Console.Write("opened");
                    MySqlCommand com = new MySqlCommand("SELECT * FROM Persons", con);// just a test command
                    MySqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())// reads through enumerable 
                    {
                        Console.WriteLine(rdr[1]);
                    }
                    rdr.Close();
                    return client; 

                }
                Console.Write("client not connected");
            }

            return null;
        }
    }
    }