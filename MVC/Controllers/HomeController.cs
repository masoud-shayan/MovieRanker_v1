using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IList<GetMoviesViewModel>>> Index()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(UserIndex));
            }

            var allMoviesRequest = _httpClientFactory.CreateClient();
            var allMoviesResponse = await allMoviesRequest.GetAsync("https://localhost:5001/api/Movie/GetMovies");
            allMoviesResponse.EnsureSuccessStatusCode();

            var allMoviesResponseContent = await allMoviesResponse.Content.ReadAsStringAsync();
            IList<GetMoviesViewModel> movies =
                JsonConvert.DeserializeObject<IList<GetMoviesViewModel>>(allMoviesResponseContent);

            // ------ fix the movie's image path
            string toBeSearched = "wwwroot";
            foreach (GetMoviesViewModel movie in movies)
            {
                if (!string.IsNullOrEmpty(movie.ImagePath))
                {
                    var imagePathTemp = movie.ImagePath;
                    imagePathTemp = imagePathTemp.Substring(imagePathTemp.IndexOf(toBeSearched) + toBeSearched.Length);
                    // imagePathTemp = Path.Combine("https://localhost:5001",imagePathTemp);
                    imagePathTemp = "https://localhost:5001/" + imagePathTemp.Replace(@"\", @"/");
                    movie.ImagePath = imagePathTemp;
                }
            }

            return View(movies);
        }

        // ------- same as Index
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserIndex()
        {
            
            
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index));
            }

            var allMoviesRequest = _httpClientFactory.CreateClient();
            var allMoviesResponse = await allMoviesRequest.GetAsync("https://localhost:5001/api/Movie/GetMovies");
            allMoviesResponse.EnsureSuccessStatusCode();

            var allMoviesResponseContent = await allMoviesResponse.Content.ReadAsStringAsync();
            IList<GetMoviesViewModel> movies =
                JsonConvert.DeserializeObject<IList<GetMoviesViewModel>>(allMoviesResponseContent);

            // ------ fix the movie's image path
            string toBeSearched = "wwwroot";
            foreach (GetMoviesViewModel movie in movies)
            {
                if (!string.IsNullOrEmpty(movie.ImagePath))
                {
                    var imagePathTemp = movie.ImagePath;
                    imagePathTemp = imagePathTemp.Substring(imagePathTemp.IndexOf(toBeSearched) + toBeSearched.Length);
                    // imagePathTemp = Path.Combine("https://localhost:5001",imagePathTemp);
                    imagePathTemp = "https://localhost:5001/" + imagePathTemp.Replace(@"\", @"/");
                    movie.ImagePath = imagePathTemp;
                }
            }

            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }


        [HttpGet]
        [Authorize]
        public IActionResult Secret()
        {
            var claims = User.Claims;
            var singleString = string.Join(",", claims);


            return Ok(new
            {
                singleString
            });
        }
    }
}