﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVC.Controllers
{
    public class UserPhotoController : Controller
    {
        // private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpClientFactory _httpClientFactory;


        public UserPhotoController(IWebHostEnvironment hostEnvironment, IHttpClientFactory httpClientFactory)
        {
            // _context = context;
            _hostEnvironment = hostEnvironment;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        // [Authorize]
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.Images.FirstAsync());
        // }

        // [HttpGet()]
        // // [Authorize]
        // public async Task<IActionResult>  Index(int? id)
        // {
        //     
        //     // should get User image from IdentityServer with idToken
        //
        //     if (id == null)
        //     {
        //         return BadRequest();
        //     }
        //     
        //     var imageModel = _context.Images
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     
        //     if (imageModel == null)
        //     {
        //         return NotFound();
        //     }
        //     
        //     return View(imageModel);
        //     return Ok();
        // }

        // [HttpGet]
        // // [Authorize]
        // public IActionResult Image()
        // {
        //     return View();
        // }

        // [HttpPost]
        // // [Authorize]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult>  Image(UserImageViewModel userImageViewModel)
        // {
        //     
        //     // save image to root/image
        //     var rootPath = _hostEnvironment.WebRootPath;
        //     var fileName = Path.GetFileNameWithoutExtension(userImageViewModel.UserImageFile.FileName);
        //     var extension = Path.GetExtension(userImageViewModel.UserImageFile.FileName);
        //     userImageViewModel.ImageName = fileName + DateTime.Now.ToString("yymmddssfff") + extension;
        //     var path = Path.Combine(rootPath + "/image/" + userImageViewModel.ImageName);
        //
        //     using (var fileStream = new FileStream(path , FileMode.Create))
        //     {
        //         await userImageViewModel.UserImageFile.CopyToAsync(fileStream);
        //     }
        //     
        //     // save image path in database
        //     UserImage userImage = new UserImage();
        //     userImage.ImageFileName = userImageViewModel.ImageName;
        //     await  _context.AddAsync(userImage);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Image));
        // }
        //
        //
        // [HttpPost, ActionName("Delete")]
        // // [Authorize]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Image(int? id)
        // {
        //
        //     if (id == null)
        //     {
        //         return BadRequest();
        //     }
        //
        //     var imageModel = await _context.Images.FindAsync(id);
        //
        //     // delete image from root/image 
        //     var rootPath = _hostEnvironment.WebRootPath;
        //     var imagePath = Path.Combine(rootPath, "image", imageModel.ImageFileName);
        //
        //     if (System.IO.File.Exists(imagePath)) System.IO.File.Delete(imagePath);
        //     else return NotFound();
        //     
        //     
        //     // delete image record from database
        //     _context.Images.Remove(imageModel);
        //     await _context.SaveChangesAsync();
        //     
        //
        //    return RedirectToAction(nameof(Image));
        // }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Photo()
        {
            // get access token
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");

            // var identityServerClient = _httpClientFactory.CreateClient();
            // identityServerClient.SetBearerToken(accessToken);
            // var response =  identityServerClient.GetAsync("https://localhost:5005/Auth/UserPhoto").Result;
            //
            // if (!response.IsSuccessStatusCode)
            // {
            //     return Unauthorized();
            // }
            //
            // var content = await response.Content.ReadAsStringAsync();


            // var id_token = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
            // var access_token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            //
            // return Ok(new
            // {
            //     id_token ,
            //     access_token
            // });
            
            
            
            return Redirect("https://localhost:5005/Auth/UserPhoto");

        }





        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Photo(UserPhotoViewModel userPhotoViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "there is no image or your image is not in correct format");
                return View();
            }
        
            if (userPhotoViewModel.UserImageFile.Length > (1048576 * 2))
            {
                ModelState.AddModelError("", "please Add an image which less than 2MB");
                return View();
            }
        
        
            // find correct End Point from IdentityServer
            var identityServerClient1 = _httpClientFactory.CreateClient();
        
            var discoveryDocument = await identityServerClient1.GetDiscoveryDocumentAsync("https://localhost:5005/");
        
            if (discoveryDocument.IsError)
            {
                return BadRequest();
            }
        
            // var tokenResponse = await identityServerClient.RequestClientCredentialsTokenAsync(
            //     new ClientCredentialsTokenRequest
            //     {
            //         Address = discoveryDocument.TokenEndpoint,
            //         ClientId = "ClientId_MVC",
            //         ClientSecret = "ClientSecret_MVC",
            //
            //         Scope = "MovieApi"
            //     });
        
        
            // get access token
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
        
        
            // call IdentityServer with access token to get Current logged in User Email
            var identityServerClient2 = _httpClientFactory.CreateClient();
        
            identityServerClient2.SetBearerToken(accessToken);
        
            var response = await identityServerClient2.GetAsync(discoveryDocument.UserInfoEndpoint);
        
            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized();
            }
        
            var content = await response.Content.ReadAsStringAsync();
            var contentDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            contentDictionary.TryGetValue("email", out string Email);
        
        
            // Call the identityServer to post user select image file 
            try
            {
                var identityServerClient3 = _httpClientFactory.CreateClient();
                // var requestContent = new MultipartFormDataContent();
            
                byte[] ImageBytes;
                using (var br = new BinaryReader(userPhotoViewModel.UserImageFile.OpenReadStream()))
                {
                    ImageBytes = br.ReadBytes((int) userPhotoViewModel.UserImageFile.OpenReadStream().Length);
                    
                    using (var requestContent = new MultipartFormDataContent())
                    {
                        requestContent.Add(new StreamContent( new MemoryStream(ImageBytes)), "UserPhoto",userPhotoViewModel.UserImageFile.FileName );
                        identityServerClient3.SetBearerToken(accessToken);
                        var result =  identityServerClient3.PostAsync("https://localhost:5005/Auth/UserPhoto", requestContent).Result;
                        return StatusCode((int)result.StatusCode); //201 Created the request has been fulfilled.
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(); // 400 is bad request
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserInfo()
        {

            var content = await GetCurrentUserEmail();
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            
            return Ok(new
            {
                accessToken
            });
        }

        
        
        private async Task<Dictionary<string,string>> GetCurrentUserEmail()
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
                // return situation;
            }


            // call IdentityServer with access token to get Current logged in User Email
            var identityServerClient2 = _httpClientFactory.CreateClient();

            identityServerClient2.SetBearerToken(accessToken);

            var response = await identityServerClient2.GetAsync(discoveryDocument.UserInfoEndpoint);

            if (!response.IsSuccessStatusCode)
            {
                situation = "Unauthorized";
                // return situation;
            }

            var content = await response.Content.ReadAsStringAsync();
            var contentDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            contentDictionary.TryGetValue("email", out string Email);
            
            

            return contentDictionary;
        }
    }
}