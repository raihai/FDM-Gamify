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
            //fetch form data
            string Username = HttpContext.Request.Form["Username"];
            string Password = HttpContext.Request.Form["Password"];
            
            //Test that you get form data
            //Console.Out.WriteLine(Username);
            //Console.Out.WriteLine(Password);
            
            AdminLogin adminLogin = new AdminLogin();
            if (adminLogin.Login(HttpContext, Username, Password))
            {
                SessionManager sessionManager = new SessionManager();
                HttpContext.Session.Set("tablename", sessionManager.stringToByte("SoftwareTesting"));
                Response.Redirect("/Admin");
            }
            else
            {
                HttpContext.Session.Set("ErrorMessage", Encoding.ASCII.GetBytes("Incorrect login details please try again."));
            }
            return;
        }
    }
}