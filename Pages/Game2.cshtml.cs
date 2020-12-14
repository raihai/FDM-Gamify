using Microsoft.AspNetCore.Mvc.RazorPages;

namespace fdm_gamify2
{
    public class Game2 : PageModel
    {
            GameManager _gameManager;
            private string AstraCss => $"bottom: {_gameManager.Astra.DistFromGround}px";
        public void OnGet()
        {
            
        }
    }
    
}