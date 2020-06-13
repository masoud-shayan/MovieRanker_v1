﻿using System;
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

namespace MVC.Controllers
{
    public class CallerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CallerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Authorize]
        // call the Api from another Api
        public async Task<IActionResult> CallApiWithApi()
        {
            // request token
            var identityServerClient = _httpClientFactory.CreateClient();
            
            var discoveryDocument = await identityServerClient.GetDiscoveryDocumentAsync("https://localhost:5005/");
            
            if (discoveryDocument.IsError)
            {
                Console.WriteLine(discoveryDocument.TokenEndpoint);
                return BadRequest();
            }
            
            
            
            var tokenResponse = await identityServerClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "ClientId_MVC",
                    ClientSecret = "ClientSecret_MVC",
            
                    Scope = "MovieApi"
                });
            
        
            
            // call api
            var apiClient = _httpClientFactory.CreateClient();
        
            apiClient.SetBearerToken(tokenResponse.AccessToken);
        
            var response = await apiClient.GetAsync("https://localhost:5001/api/movie");
        
            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized();
            }
        
        
            var content = await response.Content.ReadAsStringAsync();
        
        
            return Ok(new
            {
                // access_token = tokenResponse.AccessToken,
                message = content
            });
        }
        
        
        
        [Authorize]
        // call the Api from MVC
        public async Task<IActionResult> CallApiWithMVC()
        {
            
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");


            // call api
            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(accessToken);
            var response = await apiClient.GetAsync("https://localhost:5001/api/movie");

            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized();
            }


            var content = await response.Content.ReadAsStringAsync();

            // var claim = HttpContext.User.Claims.First(c => c.Type == "preferred_username");
            // var emailAddress = claim.Value;

            return Ok(new
            {
                access_token = accessToken ,
                id_token = idToken,
                message = content
            });
        }
    }
}