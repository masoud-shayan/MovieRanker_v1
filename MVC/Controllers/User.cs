﻿﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class User : Controller
    {
        // GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Change_Settings()
        {
            return View();
        }
        
        [HttpGet("name")]
        public async Task<IActionResult> Change_Settings(string name)
        {
            return View(); // redirect to to changes in identity
        }
        
        [HttpGet]
        public  IActionResult SingOut()
        {
            return Redirect(nameof(Change_Settings)); // redirect to sign out in identity
        }
        
        [HttpGet]
        public  IActionResult SignIn()
        {
            return Redirect(nameof(Change_Settings)); // redirect to sign in in identity
        }
        
        [HttpGet]
        public  IActionResult SignUp()
        {
            return Redirect(nameof(Change_Settings)); // redirect to sign up in identity
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Added_Movies()
        {
            return View();
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Ranked_Movies()
        {
            return View();
        }
    }
}