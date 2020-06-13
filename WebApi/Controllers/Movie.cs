﻿﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Movie : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return Ok(
                new
                {
                    access_token = "salam",
                    message = "man khoobam"
                }
                );
        }
    }
}