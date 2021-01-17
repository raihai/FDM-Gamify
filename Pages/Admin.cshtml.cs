using System;
using System.Data;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace fdm_gamify2.Pages
{
    public class Admin : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.Get("IsAdmin").ToString() == "true")
            {
                DatabaseConnection dc = new DatabaseConnection();
                const string query = "SELECT * FROM Persons";
            
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
        public async void OnPost()
        {
            Console.WriteLine("Reached Delete Method");
            DatabaseConnection dc = new DatabaseConnection();
            dc.OpenConnection();
            string id = HttpContext.Request.Form["PersonToDelete"];
            string query = $"DELETE FROM Persons WHERE PersonID='{id}'";
            
            dc.ExecuteQuery(query);
            dc.CloseConnection();
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