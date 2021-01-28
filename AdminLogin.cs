using System;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Crypto.Tls;
using Ubiety.Dns.Core;

namespace fdm_gamify2
{
    public class AdminLogin
    {
        public Boolean Login(HttpContext httpContext, string Username, string Password)
        {
            //Establish database connection
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.OpenConnection();
            
            //Encrypt both username and password.
            Byte[] EncryptedUsername = Encoding.UTF8.GetBytes(Username);
            Byte[] EncryptedPassword = Encoding.UTF8.GetBytes(Password);
            MD5 md5 = new MD5CryptoServiceProvider();
            
            //Encrypting Username.
            md5.ComputeHash(EncryptedUsername);
            EncryptedUsername = md5.Hash;

            //Encrypting Password.
            md5.ComputeHash(EncryptedPassword);
            EncryptedPassword = md5.Hash;

            //Get every row for the database, and then check each row against log in details.
            DataTable dataTable = databaseConnection.GetDataTable("SELECT * FROM AdminUsers");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
               byte[] DatabaseUsername = (byte[]) dataTable.Rows[i]["Username"];
               byte[] DatabasePassword = (byte[]) dataTable.Rows[i]["Password"];
               //Check if there's a match. If there is, then store the fact that they are an admin in the session.
                if (Comparitor(DatabaseUsername, DatabasePassword, EncryptedUsername, EncryptedPassword))
                {
                    SessionManager session = new SessionManager();
                    session.NewUser(httpContext, session, "true");
                    databaseConnection.CloseConnection();
                    return true;
                } 
            } 
            databaseConnection.CloseConnection();
            return false;
        }
        
        public bool Comparitor(Byte[] dbUsername, Byte[] dbPassword, Byte[] inputUsername, Byte[] inputPassword)
        {
            //to compare two byte arrays we had to manually check each individual item
            Boolean Match = true;
            for (int i = 0; i < dbUsername.Length; i++)
            {
                if (!dbUsername[i].Equals(inputUsername[i]))
                {
                    Match = false;
                }
                if (!dbPassword[i].Equals(inputPassword[i]))
                {
                    Match = false;
                }
            }
            return Match;
        }
    }
}
