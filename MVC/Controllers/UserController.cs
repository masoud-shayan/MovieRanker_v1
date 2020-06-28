using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IWebHostEnvironment hostEnvironment, IHttpClientFactory httpClientFactory)
        {
            _hostEnvironment = hostEnvironment;
            _httpClientFactory = httpClientFactory;
        }

        // GET
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userInfo = await GetUserInfo();
            if (userInfo.Equals("BadRequest"))
            {
                return BadRequest();
            }
            else if (userInfo.Equals("Unauthorized"))
            {
                return Unauthorized();
            }

            var userInfoModel = await SetUserInfo(userInfo);

            return View(userInfoModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Change_Settings()
        {
            var userInfo = await GetUserInfo();
            if (userInfo.Equals("BadRequest"))
            {
                return BadRequest();
            }
            else if (userInfo.Equals("Unauthorized"))
            {
                return Unauthorized();
            }

            var userInfoModel = await SetUserInfo(userInfo);

            Console.WriteLine("username    :  " + userInfoModel.UserName);


            // ------- call the movie Api to sync user details with identityserver's user details
            var access_token = await HttpContext.GetTokenAsync("access_token");

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(access_token);
            var userInfoModelContent = new StringContent(JsonConvert.SerializeObject(userInfoModel), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            var httpResponse =
                await apiClient.PostAsync("https://localhost:5001/api/Movie/UpdateUser", userInfoModelContent);

            httpResponse.EnsureSuccessStatusCode();


            return View(userInfoModel);
        }


        [HttpGet("{name}")]
        [Authorize]
        public async Task<IActionResult> Change_Settings(string name)
        {
            if (name.Equals("photo"))
            {
                return Redirect("https://localhost:5005/Auth/UserPhoto");
            }
            else if (name.Equals("email"))
            {
                return Redirect("https://localhost:5005/Auth/UserEmail");
            }
            else if (name.Equals("password"))
            {
                return Redirect("https://localhost:5005/Auth/UserPassword");
            }


            return View(); // redirect to to changes in identity
        }

        [HttpGet]
        public IActionResult SingOut()
        {
            // return Redirect(nameof(Change_Settings)); // redirect to sign out in identity
            return SignOut("Cookie", "oidc");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return Redirect("https://localhost:5005/Auth/login"); // redirect to sign in in identity
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return Redirect("https://localhost:5005/Auth/register"); // redirect to sign up in identity
        }


        [HttpGet]
        public async Task<IActionResult> Added_Movies()
        {
            
            // ------- call the movie Api to get all the movies
            var access_token = await HttpContext.GetTokenAsync("access_token");

            var allMoviesRequest = _httpClientFactory.CreateClient();
            allMoviesRequest.SetBearerToken(access_token);
            var allMoviesResponse = await allMoviesRequest.GetAsync("https://localhost:5001/api/Movie/AddedMoviesByUser");
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


        [HttpGet]
        public async Task<IActionResult> Ranked_Movies()
        {
            // ------- call the movie Api to get all the movies
            var access_token = await HttpContext.GetTokenAsync("access_token");

            var allMoviesRequest = _httpClientFactory.CreateClient();
            allMoviesRequest.SetBearerToken(access_token);
            var allMoviesResponse = await allMoviesRequest.GetAsync("https://localhost:5001/api/Movie/RankedMoviesByUser");
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


        private async Task<string> GetUserInfo()
        {
            string situation = "";

            // get access token
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");


            // find correct End-Point from IdentityServer
            var identityServerClient1 = _httpClientFactory.CreateClient();

            var discoveryDocument = await identityServerClient1.GetDiscoveryDocumentAsync("https://localhost:5005/");

            if (discoveryDocument.IsError)
            {
                situation = "BadRequest";
                return situation;
            }


            // call IdentityServer with access token to get Current logged in User Email
            var identityServerClient2 = _httpClientFactory.CreateClient();

            identityServerClient2.SetBearerToken(accessToken);

            var response = await identityServerClient2.GetAsync(discoveryDocument.UserInfoEndpoint);

            if (!response.IsSuccessStatusCode)
            {
                situation = "Unauthorized";
                return situation;
            }

            var userInfo = await response.Content.ReadAsStringAsync();


            return userInfo;
        }

        private async Task<UserInfoViewModel> SetUserInfo(string userInfo)
        {
            var userInfoDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(userInfo);
            userInfoDictionary.TryGetValue("email", out string email);
            userInfoDictionary.TryGetValue("UserImagePath", out string UserImagePath);
            userInfoDictionary.TryGetValue("sub", out string userId);


            var userImagePathNormalizer = "";


            // if user has not specified any avatar
            if (string.IsNullOrEmpty(UserImagePath))
            {
                //------ set UserImagePath to root/image/avatar.jpg

                //------ on development
                userImagePathNormalizer = "/image/avatar.jpg";

                //------- on production
                // var rootPath = _hostEnvironment.WebRootPath;
                // UserImagePath = Path.Combine(rootPath + "/image/avatar.jpg");
            }
            else
            {
                string toBeSearched = "wwwroot";
                userImagePathNormalizer =
                    UserImagePath.Substring(UserImagePath.IndexOf(toBeSearched) + toBeSearched.Length);
                userImagePathNormalizer = Path.Combine("https://localhost:5005", userImagePathNormalizer);
                userImagePathNormalizer = "https://localhost:5005/" + userImagePathNormalizer.Replace(@"\", @"/");
            }

            var UserInfoModel = new UserInfoViewModel
            {
                Email = email,
                UserName = email.Substring(0, email.IndexOf("@")),
                UserImagePath = userImagePathNormalizer,
                UserId = new Guid(userId)
            };


            return UserInfoModel;
        }
    }
}