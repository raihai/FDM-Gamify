using System;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace fdm_gamify2.Pages
{
    public class Admin_Login : PageModel
    {
        public void OnGet()
        {
            
        }

        public async void OnPost()
        {
            AdminLogin adminLogin = new AdminLogin();
            if (adminLogin.Login(HttpContext))
            {
                Response.Redirect("./Admin.cshtml");
            }
            else
            {
                HttpContext.Session.Set("ErrorMessage", Encoding.ASCII.GetBytes("Incorrect login details please try again"));
            }
            return;
        }
    }
}