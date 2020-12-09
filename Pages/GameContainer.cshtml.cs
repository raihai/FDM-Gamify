using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace fdm_gamify2
{
    public class GameContainer : PageModel
    {
        
        GameManager _gameManager = new GameManager();
        string AstraCss => $"bottom: {_gameManager.Astra.DistFromGround}px";
        
        public void OnGet()
        {

        }
        
    }

}