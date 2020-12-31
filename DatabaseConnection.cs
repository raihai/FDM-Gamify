﻿﻿using System;
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
        
        //private string _conString = "Data Source=cs-db.ncl.ac.uk;Initial Catalog=t2033t26;User id=t2033t26;Password=Sit-HewsRide";
        private const string ConString = "SERVER= localhost;UID=t2033t26;PASSWORD=Sit-HewsRide;DATABASE=t2033t26";
        private MySqlConnection _connection;
        private SshClient _client;

        // opens connection to database
        public void OpenConnection()
        {
            Console.WriteLine("Start of open connection");
            _client = SshTunnel();
            Console.Write("hanging...");
            //_client.RunCommand("mysql -h cs-db.ncl.ac.uk -u t2033t26 -p Sit-HewsRide");
            Console.Write("done");
            ForwardedPortLocal portForwarded = new ForwardedPortLocal("localhost", 3306, "cs-db.ncl.ac.uk", 3306);;
            Console.WriteLine(portForwarded.IsStarted);
            _client.AddForwardedPort(portForwarded);
            portForwarded.Start();
            Console.Write(portForwarded.IsStarted);
            _connection = new MySqlConnection(ConString);
            _connection.Open();
            Console.WriteLine("opened");
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
        public MySqlDataReader DataReader(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, _connection); ;
            return cmd.ExecuteReader();

        }

        // shows data returned by query in a grid view
        public DataTable GetDataTable(string query)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, ConString);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet.Tables[0];

            
            /*Console.Write(Connection.Database);
            DataTable dt = new DataTable();
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
            {
                Console.WriteLine(dt);
                da.Fill(dt);
            }

            return dt;*/
        }

        /*
         *Creates SSH Tunnel to linux server
         * Replace Username and Passowrd in connection info variable to your own.
         * Once connected connects to shared db using a port from local host -> cs.db
         * Connects to SQL DB
         * 
         */
        private static SshClient SshTunnel()
        {
            string username = "b9012721";
            string password = "Intensetryterms1";
            Console.WriteLine("Start of method");
            var connectionInfo = new SshClient("cs-linux.ncl.ac.uk",
                username, password);
            var client = connectionInfo;
            client.Connect();
                if (client.IsConnected)
                {
                    Console.WriteLine("client is connected");
                    return client;
                }
                Console.Write("client not connected");

                return null;
        }
    }
    }