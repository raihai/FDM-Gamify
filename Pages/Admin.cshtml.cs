using System;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace fdm_gamify2.Pages
{
    public class Admin : PageModel
    {
        public void OnGet()
        {
            if(Request.Cookies["userType"] != "Admin")
            {
                Response.Redirect("/error");
                return;
            }
            // check that the admin has logged in to access this page if not it redirects them to an error page
            DatabaseConnection dc = new DatabaseConnection();
            const string query = "SELECT * FROM Persons";
            
            // open connection to database and gets a datatable from above query
            dc.OpenConnection();
            DataTable dt = dc.GetDataTable(query);

            // retrieves new html body created from datatable and writes it to Leaderboard.cshtml
            string htmlBody = ConvertDataTableToHtml(dt);
            System.IO.File.WriteAllText(@"Admin.cshtml", htmlBody);
            
            // closes connection to the database
            dc.CloseConnection();
        }
        
         // Converts the datatable to html to be shown on the web page
        private static string ConvertDataTableToHtml(DataTable dt)
        {
            StringBuilder builder = new StringBuilder();

            // appends page model, page title & toolbar to StringBuilder
            builder.Append("@page '/Admin'");
            builder.Append("@model Admin");
            builder.Append("@{ViewData['Title'] = 'Admin'; }");
            builder.Append("<section class='fdm-header-banner reduced-height2'>");
            builder.Append("<div  class = 'text-center fdm'>");
            builder.Append("<h1 itemprop='name' class='banner-heading2'><span class='font-weight-bold'>Admin Page</span></h1><br>");
            builder.Append("</div>");
            builder.Append("</section>");
            
            // appends table class to StringBuilder
            builder.Append("<section class='secondSelection'>");
            builder.Append("<div class='container'>");
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
            builder.Append("</div>");
            builder.Append("</section>");
            
            /*// appends the update and delete buttons
            builder.Append("</div>");
            builder.Append("<input type='button' value='Update User' data-bind='click: $root.update'/>");
            builder.Append("<input type='button' value='Delete User' data-bind='click: $root.delete'/>");
            builder.Append("</div>");*/
            
            // returns completed string
            return builder.ToString();
        }
    }
}