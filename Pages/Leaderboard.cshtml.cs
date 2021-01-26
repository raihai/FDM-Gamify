using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Renci.SshNet;

namespace fdm_gamify2.Pages
{
    public class Leaderboard : PageModel
    {
        public void OnGet()
        {
            DatabaseConnection dc = new DatabaseConnection();
            string tablename = HttpContext.Request.Cookies["QuizComplete"];
            string query = "SELECT nickname, points FROM " + tablename +" ORDER BY points DESC LIMIT 10";
            // open connection to database and gets a datatable from above query
            dc.OpenConnection();
            DataTable dt = dc.GetDataTable(query);

            // retrieves new html body created from datatable and writes it to Leaderboard page
            string htmlBody = ConvertDataTableToHtml(dt);
            System.IO.File.WriteAllText(@"leaderboardTable.html", htmlBody);
             
            // closes connection to the database
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