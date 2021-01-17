﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace fdm_gamify2.Pages
{
    public class IndexModel : PageModel
    {
        
        GameManager _gameManager = new GameManager();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
		
	
	
        public void OnGet()
        {
            SessionManager session = new SessionManager();
            session.NewUser(HttpContext, session, "false");
        }
    }
}
