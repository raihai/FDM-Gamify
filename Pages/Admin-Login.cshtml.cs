using Microsoft.AspNetCore.Mvc.RazorPages;

namespace fdm_gamify2.Pages
{
    public class Admin_Login : PageModel
    {
        public void OnGet()
        {
            
        }

        public void Login()
        {
            AdminLogin adminLogin = new AdminLogin();
            if (adminLogin.Login(HttpContext))
            {
                Response.Redirect("./Admin.cshtml");
            }
            else
            {
                //output failed to login message
            }
        }
    }
}