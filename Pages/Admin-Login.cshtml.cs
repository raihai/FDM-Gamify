using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace fdm_gamify2.Pages
{
    public class Admin_Login : PageModel
    {
        public void OnGet()
        {
            
        }

        public static String Login(HttpContext HttpContext, HttpResponse Response, HttpRequest HttpRequest)
        {
            AdminLogin adminLogin = new AdminLogin();
            if (adminLogin.Login(HttpContext))
            {
                Response.Redirect("./Admin.cshtml");
            }
            else
            {
                
            }
            return "";
        }
    }
}