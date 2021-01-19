using System;
using System.Data;
using System.IO;
using System.Net;
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
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.OpenConnection();
            DataTable dataTable = databaseConnection.GetDataTable("SELECT * FROM AdminUsers");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Byte[] EncryptedUsername = Encoding.ASCII.GetBytes(Username);
                Byte[] EncryptedPassword = Encoding.ASCII.GetBytes(Password);
                MD5 md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(EncryptedUsername);
                EncryptedUsername = md5.Hash;
                md5.ComputeHash(EncryptedPassword);
                EncryptedPassword = md5.Hash;
                Console.Out.WriteLine("\n" + System.Text.Encoding.ASCII.GetString(EncryptedUsername));
                Console.Out.WriteLine(System.Text.Encoding.ASCII.GetString(EncryptedPassword) + "\n");
                if (Username == dataTable.Rows[i]["Username"].ToString() && Password == dataTable.Rows[i]["Password"].ToString())
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
    }
}
