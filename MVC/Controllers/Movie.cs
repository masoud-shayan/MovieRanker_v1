﻿﻿using System.Threading.Tasks;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class Movie : Controller
    {
        // GET
        [Route("[controller]")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        [Route("[controller]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            return View();
        }
        
        [Route("[controller]/{id:int}/[action]")]
        [HttpGet]
        public async Task<IActionResult>  Who_Added_It(int id)
        {
            ViewData["data"] = id;
            return View();
        }
        
        [Route("[controller]/{id:int}/[action]")]
        [HttpGet]
        public async Task<IActionResult>  Who_Ranked_It(int id)
        {
            return View();
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Add_New_Movie()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Add_New_Movie(int i)
        {
            return View();
        }
        
        
        [HttpGet]
        public IActionResult Delete_Movie()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete_Movie(int i)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Rank_Movie(int i)
        {
            return Redirect(nameof(Index));
        }
    }
}