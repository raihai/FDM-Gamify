using System;
using System.Data;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace fdm_gamify2.Pages
{
    public class Admin : PageModel
    {
        public void OnGet()
        {
            if (System.Text.Encoding.Default.GetString(HttpContext.Session.Get("IsAdmin")) == "true")
            {
                SessionManager sessionManager = new SessionManager();
                string tablename = sessionManager.toString(HttpContext.Session.Get("tablename"));
                DatabaseConnection dc = new DatabaseConnection();
                string query = "SELECT playerID,nickname, points FROM " +tablename+" ORDER BY points DESC LIMIT 10";
            
                // open connection to database and gets a datatable from above query
                dc.OpenConnection();
                DataTable dt = dc.GetDataTable(query);

                // retrieves new html body created from datatable and writes it to Leaderboard.cshtml
                string htmlBody = ConvertDataTableToHtml(dt);
                System.IO.File.WriteAllText(@"AdminTable.html", htmlBody);
            
                // closes connection to the database
                dc.CloseConnection();

            }
            else
            {
                Response.Redirect("./Error");
            }
        }

        [HttpPost]
        public async void OnPost()// when the form to change tables is submitted
        {
            Console.WriteLine("on post");
            DatabaseConnection dc = new DatabaseConnection();
            dc.OpenConnection();
            if (HttpContext.Request.Form.ContainsKey("tablename"))
            {
                string tablename = HttpContext.Request.Form["tablename"];// gets the table choice from the form
                SessionManager sessionManager = new SessionManager();
                HttpContext.Session.SetString("tablename", tablename);
            }
            else
            {
                string id = HttpContext.Request.Form["PersonToDelete"];
                //string query = "DELETE FROM " + tablename + " WHERE playerID=" + id + ";";
                string tablename = HttpContext.Session.GetString("tablename");
                string query = $"DELETE FROM {tablename} WHERE playerID='{id}'";

                dc.ExecuteQuery(query);
            }
            dc.CloseConnection();
            OnGet();
            HttpContext.Response.Redirect(HttpContext.Request.GetDisplayUrl());;
        }
        
         // Converts the datatable to html to be shown on the web page
        private static string ConvertDataTableToHtml(DataTable dt)
        {
            StringBuilder builder = new StringBuilder();

            // appends table class
            builder.Append("<table class='blackTable'>");
            builder.Append("<thead>");
            
            // appends header row to StringBuilder
            builder.Append("<tr>") ;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                builder.Append("<th>"+dt.Columns[i].ColumnName+"</th>");
            }
            builder.Append("</tr>");
            builder.Append("</thead>");
            
            // appends item rows to StringBuilder
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                builder.Append("<tr>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    builder.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");
                }
                builder.Append("</tr>");
            }
            
            // appends closing html to builder
            builder.Append("</table>");
            
            // returns completed string
            return builder.ToString();
        }
 
            
    }
}