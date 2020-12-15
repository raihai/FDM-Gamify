using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace fdm_gamify2.Pages
{
    public class Leaderboard : PageModel
    {
        
        public void OnGet()
        {
            DatabaseConnection dc = new DatabaseConnection();
            const string query = "SELECT Nickname, Score FROM leaderboard ORDER BY Score DESC LIMIT 10";
            
            // open connection to database and gets a datatable from above query
            dc.OpenConnection();
            DataTable dt = dc.GetDataTable(query);

            // retrieves new html body created from datatable and writes it to Leaderboard.cshtml
            string htmlBody = ConvertDataTableToHtml(dt);
            System.IO.File.WriteAllText(@"Leaderboard.cshtml", htmlBody);
        }
        
        // Converts the datatable to html to be shown on the web page
        private static string ConvertDataTableToHtml(DataTable dt)
        {
            string html = "<table>";

            //add header row
            html += "<tr>";
            for(int i=0;i<dt.Columns.Count;i++)
                html+="<td>"+dt.Columns[i].ColumnName+"</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j< dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
    }
}