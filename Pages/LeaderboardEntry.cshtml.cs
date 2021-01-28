using System;
using System.Net;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace fdm_gamify2.Pages
{
    public class LeaderboardEntry : PageModel
    {    

        SessionManager sessionManager = new SessionManager();
        public void OnGet()
        {
            
        }

        public async void OnPost()
        {// when they submit their nickname
            Console.WriteLine("post");
            string Nickname  = HttpContext.Request.Form["nickname"];
            sessionManager.newUser(Nickname,Int32.Parse(HttpContext.Request.Cookies["Points"]), HttpContext.Request.Cookies["QuizComplete"]);
            HttpContext.Response.Cookies.Delete("Points");
            HttpContext.Response.Redirect("/Leaderboard");
            
        }
    }
}