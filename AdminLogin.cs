using System;
using System.Data;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using MySqlX.XDevAPI.Relational;
using Ubiety.Dns.Core;

namespace fdm_gamify2
{
    public class AdminLogin
    {
        public Boolean Login(HttpContext httpContext)
        {
            String Username = "";
            String Password = "";
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.OpenConnection();
            DataTable dataTable = databaseConnection.GetDataTable("SELECT * FROM AdminUsers");
            for (int i = 0; i < dataTable.Rows.Count - 1; i++)
            {
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
