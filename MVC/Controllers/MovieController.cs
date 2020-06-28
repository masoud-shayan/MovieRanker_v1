using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

namespace MVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MovieController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET
        [Route("[controller]")]
        [HttpGet]
        public async Task<ActionResult<IList<GetMoviesViewModel>>> Index()
        {
            // ------- call the movie Api to get all the movies
            var access_token = await HttpContext.GetTokenAsync("access_token");

            var allMoviesRequest = _httpClientFactory.CreateClient();
            allMoviesRequest.SetBearerToken(access_token);
            var allMoviesResponse = await allMoviesRequest.GetAsync("https://localhost:5001/api/Movie/GetMovies");
            allMoviesResponse.EnsureSuccessStatusCode();

            var allMoviesResponseContent = await allMoviesResponse.Content.ReadAsStringAsync();
            IList<GetMoviesViewModel> movies =
                JsonConvert.DeserializeObject<IList<GetMoviesViewModel>>(allMoviesResponseContent);

            // ------ fix the movie's image path
            string toBeSearched = "wwwroot";
            foreach (GetMoviesViewModel movie in movies)
            {
                var imagePathTemp = movie.ImagePath;
                imagePathTemp = imagePathTemp.Substring(imagePathTemp.IndexOf(toBeSearched) + toBeSearched.Length);
                // imagePathTemp = Path.Combine("https://localhost:5001",imagePathTemp);
                imagePathTemp = "https://localhost:5001/" + imagePathTemp.Replace(@"\", @"/");
                movie.ImagePath = imagePathTemp;
            }


            return View("Movies", movies);
        }

        [Route("[controller]/{movieName}")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<RetrieveMovieViewModel>> Index(string movieName)
        {
            var access_token = await HttpContext.GetTokenAsync("access_token");
            ViewData["access_token"] = access_token; // for delete button
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId


            // ------- call the movie Api to get 1 movie
            var allMoviesRequest = _httpClientFactory.CreateClient();
            allMoviesRequest.SetBearerToken(access_token);
            var allMoviesResponse =
                await allMoviesRequest.GetAsync($"https://localhost:5001/api/Movie/GetMovie/{movieName}");
            allMoviesResponse.EnsureSuccessStatusCode();

            var allMoviesResponseContent = await allMoviesResponse.Content.ReadAsStringAsync();
            RetrieveMovieViewModel retrieveMovie =
                JsonConvert.DeserializeObject<RetrieveMovieViewModel>(allMoviesResponseContent);


            // ------ fix the movie's image path
            if (!string.IsNullOrEmpty(retrieveMovie.ImagePath))
            {
                string toBeSearched = "wwwroot";
                var imagePathTemp = retrieveMovie.ImagePath;
                imagePathTemp = imagePathTemp.Substring(imagePathTemp.IndexOf(toBeSearched) + toBeSearched.Length);
                // imagePathTemp = Path.Combine("https://localhost:5001",imagePathTemp);
                imagePathTemp = "https://localhost:5001/" + imagePathTemp.Replace(@"\", @"/");
                retrieveMovie.ImagePath = imagePathTemp;
            }

            return View("movie", retrieveMovie);
        }

        // [Route("[controller]/{id:int}/[action]")]
        [HttpGet]
        public async Task<IActionResult> Who_Added_It(string movieName)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");


            var movieClient = _httpClientFactory.CreateClient();
            movieClient.SetBearerToken(accessToken);

            var httpResponse = await movieClient.GetAsync($"https://localhost:5001/api/Movie/MovieAddedBy/{movieName}");
            httpResponse.EnsureSuccessStatusCode();

            var user = await httpResponse.Content.ReadAsStringAsync();
            



            var userInfoDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(user);
            userInfoDictionary.TryGetValue("id", out string userId);
            userInfoDictionary.TryGetValue("userName", out string userName);
            userInfoDictionary.TryGetValue("userImage", out string userImage);


            var userInfo = new UserInfoViewModel
            {
                UserId = new Guid(userId),
                UserName = userName,
                Email = userName,
                UserImagePath = userImage
            };


            return View(userInfo);
        }

        // [Route("[controller]/{id:int}/[action]")]
        [HttpGet]
        public async Task<IActionResult> Who_Ranked_It(string movieName)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");


            var movieClient = _httpClientFactory.CreateClient();
            movieClient.SetBearerToken(accessToken);

            var httpResponse = await movieClient.GetAsync($"https://localhost:5001/api/Movie/MovieRankedBy/{movieName}");
            httpResponse.EnsureSuccessStatusCode();

            var users = await httpResponse.Content.ReadAsStringAsync();
            



            var usersInfoDictionary = JsonConvert.DeserializeObject<IList<Dictionary<string, string>>>(users);

            
            IList<UserInfoViewModel> usersList = new List<UserInfoViewModel>();

            foreach (var userDic in usersInfoDictionary)
            {
                usersList.Add(new UserInfoViewModel
                {
                    UserId = new Guid(userDic["id"]),
                    UserName = userDic["userName"],
                    Email = userDic["userName"],
                    UserImagePath = userDic["userImage"]
                });
            }


            return View(usersList);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add_New_Movie()

        {
            // var accessToken =  HttpContext.GetTokenAsync("access_token").Result;
            // return Ok(new
            // {
            //
            //     accessToken
            // });
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_New_Movie(AddMovieViewModel movieModel)
        {
            // -------- image file validations
            if (!ModelState.IsValid)
            {
                return View(movieModel);
            }

            var ext = Path.GetExtension(movieModel.Image.FileName);

            if (!ext.Equals(".jpg") && !ext.Equals(".png") && !ext.Equals(".jpeg") && !ext.Equals(".bmp"))
            {
                ModelState.TryAddModelError("",
                    "The Image File  only accepts files with the following extensions: .jpg, .png, .jpeg, .bmp");

                return View(movieModel);
            }


            // -------- sync current User with the Api
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


            var accessToken = await HttpContext.GetTokenAsync("access_token");


            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(accessToken);
            var userInfoModelContent = new StringContent(JsonConvert.SerializeObject(userInfoModel), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            var httpResponse =
                await apiClient.PostAsync("https://localhost:5001/api/Movie/UpdateUser", userInfoModelContent);

            httpResponse.EnsureSuccessStatusCode();


            // ------- call the Api to add new movie with this current User


            var createMovieClient = _httpClientFactory.CreateClient();
            createMovieClient.SetBearerToken(accessToken);

            using (var content = new MultipartFormDataContent())
            {
                using (var memoryStream = new MemoryStream())
                {
                    await movieModel.Image.CopyToAsync(memoryStream);
                    var byteStream = memoryStream.ToArray();


                    content.Add(new StringContent(movieModel.Name), "Name");
                    content.Add(new StringContent(movieModel.Description), "Description");
                    content.Add(new StringContent(movieModel.ReleaseDate.ToString()), "ReleaseDate");
                    content.Add(new StringContent(movieModel.Director), "Director");
                    content.Add(new StringContent(userInfoModel.UserId.ToString()), "UserId");
                    content.Add(new StreamContent(new MemoryStream(byteStream)), "ImageFile",
                        movieModel.Image.FileName);

                    var createMovieResponse =
                        await createMovieClient.PostAsync("https://localhost:5001/api/Movie/AddMovie", content);
                    var createMovieResponseContent = await createMovieResponse.Content.ReadAsStringAsync();

                    httpResponse.EnsureSuccessStatusCode();

                    // return Ok(new
                    // {
                    //     createMovieResponseContent
                    // });

                    return RedirectToAction(nameof(Add_New_Movie_Success));
                }
            }


            return RedirectToAction(nameof(Add_New_Movie_Success));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete_Movie()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Movie(string movieName)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");


            var movieClient = _httpClientFactory.CreateClient();
            movieClient.SetBearerToken(accessToken);

            // ------- 1. with anonymous  type (class)
            // var movieRankContent = new StringContent(JsonConvert.SerializeObject(new {MovieName = movieName}), Encoding.UTF8, MediaTypeNames.Application.Json);

            // ....... 2. with a named type (class)
            // var movieRankContent = new StringContent(movieName, Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse =
                await movieClient.DeleteAsync($"https://localhost:5001/api/Movie/DeleteMovie/{movieName}");

            httpResponse.EnsureSuccessStatusCode();
            return View();
        }

        [HttpGet]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rank_Movie()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rank_Movie(int RankInputId, string movieName)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");


            var movieClient = _httpClientFactory.CreateClient();
            movieClient.SetBearerToken(accessToken);

            // ------- 1. with anonymous  type (class)
            // var movieRankContent = new StringContent(JsonConvert.SerializeObject(new {MovieName = movieName , MovieRank = RankInputId }), Encoding.UTF8, MediaTypeNames.Application.Json);

            // ....... 2. with a named type (class)
            var movieRankContent =
                new StringContent(
                    JsonConvert.SerializeObject(new MovieRankModel {MovieName = movieName, MovieRank = RankInputId}),
                    Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse =
                await movieClient.PostAsync("https://localhost:5001/api/Movie/RankMovie", movieRankContent);

            httpResponse.EnsureSuccessStatusCode();
            return View();
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
            userInfoDictionary.TryGetValue("sub", out string userId);
            userInfoDictionary.TryGetValue("email", out string email);
            userInfoDictionary.TryGetValue("UserImagePath", out string UserImagePath);

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

            var UserInfoModel = new UserInfoViewModel();

            UserInfoModel.UserId = new Guid(userId);
            UserInfoModel.Email = email;
            UserInfoModel.UserName = email.Substring(0, email.IndexOf("@"));
            UserInfoModel.UserImagePath = userImagePathNormalizer;


            return UserInfoModel;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add_New_Movie_Success()
        {
            // var accessToken = HttpContext.GetTokenAsync("access_token").Result;
            // return Ok(new
            // {
            //     accessToken
            // });

            return View();
        }

        private string ImageUrlNormalizer(string host, string imageUrl)
        {

            if (imageUrl.Equals("/image/avatar.jpg"))
            {
                return Path.Combine("https://localhost:5003", "wwwroot", imageUrl);
            }
            string toBeSearched = "wwwroot";

            var imagePath = imageUrl;
            imagePath = imagePath.Substring(imagePath.IndexOf(toBeSearched) + toBeSearched.Length);
            // imagePathTemp = Path.Combine("https://localhost:5001",imagePathTemp);
            imagePath = "host" + imagePath.Replace(@"\", @"/");

            return imagePath;
        }
    }
}