﻿using System;
using System.Data;
using System.Net;
using Microsoft.AspNetCore;
using MySqlX.XDevAPI.Relational;
using Ubiety.Dns.Core;

namespace fdm_gamify2
{
    public class AdminLogin
    {
        public Boolean Login()
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
                    Cookie cookie= new Cookie();
                    var request = (HttpWebRequest)WebRequest.Create("");
                    request.CookieContainer = new CookieContainer();
                    cookie.Value = "AdminUser";
                    request.CookieContainer.Add(cookie);
                    return true;
                }
            }
            return false;
        }
    }
}
