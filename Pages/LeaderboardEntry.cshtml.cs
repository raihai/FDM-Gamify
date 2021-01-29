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
            string Nickname  = HttpContext.Request.Form["nickname"];// gets data from the user submitted form
            if (fdm_gamify2.ServerFilter.ContainsBadChars(Nickname))
            {
                Response.Redirect("./ServerFilterError");// if the name contains bad characters they are redirected
            }
            sessionManager.newUser(Nickname,Int32.Parse(HttpContext.Request.Cookies["Points"]), HttpContext.Request.Cookies["QuizComplete"]);// creates a new user
            HttpContext.Response.Cookies.Delete("Points");// remove the points cookie so its cleared for next game try
            HttpContext.Response.Redirect("/Leaderboard");// send them to the leaderboard

        }
    }
}