﻿using System;
using System.IO;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _hostEnvironment;


        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager , IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if(!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            
            
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, true , false);
            
            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }
            
            // Added to active Lockout feature if the LockoutEnabled == 1 in DB
            if (result.IsLockedOut)
            {
                // send email/phone message to confirm he/she is identified
                // just for avoiding compiler return error
                ModelState.AddModelError("", "there is no IsLockedOut mechanism");
                return View();
            }
            
            // Added to active 2 step Verification if the TwoFactorEnabled == 1 in DB
            if(result.RequiresTwoFactor)
            {
                // just for avoiding compiler return error
                ModelState.AddModelError("", "there is no RequiresTwoFactor mechanism");
                return View();            
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }
        
        
        
        
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl)
        {
            if(!ModelState.IsValid)
            {
                return View(registerViewModel);
            }


            var user = new User()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
                // UserName = registerViewModel.Email.Substring(0, registerViewModel.Email.IndexOf("@") )
            }; 
            
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
 
                return View(registerViewModel);
            }
            
            
            // var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
            //
            // var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
            // await _emailSender.SendEmailAsync(message);
            //
            //
            // await _userManager.AddToRoleAsync(user, "Visitor");

            await _signInManager.SignInAsync(user, false);
            return Redirect(returnUrl);
        }

        
        // change or set UserPhoto
        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> UserPhoto()
        {
            
            // just for commenting to know !!
            // var email1 = HttpContext.User.FindFirst("email")?.Value;
            // var email2 = _signInManager.Context.User.FindFirst("email").Value;
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            //
            //
            // var applicationUser = await _userManager.GetUserAsync(User);
            // string email3 = applicationUser?.Email; // will give the user's Email
            //
            //
            // return Ok(new
            // {
            //     email3
            // });
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserPhoto(UserPhotoViewModel userPhotoViewModel)
        {
                // save image to root/image
                var rootPath = _hostEnvironment.WebRootPath;
                var fileName = Path.GetFileNameWithoutExtension(userPhotoViewModel.UserImageFile.FileName);
                var extension = Path.GetExtension(userPhotoViewModel.UserImageFile.FileName);
                var userPhotoName = fileName + DateTime.Now.ToString("yymmddssfff") + extension;
                var path = Path.Combine(rootPath + "/image/" + userPhotoName);
            
                using (var fileStream = new FileStream(path , FileMode.Create))
                {
                    await userPhotoViewModel.UserImageFile.CopyToAsync(fileStream);
                }
                
                // save image path in database
                
                var currentUser = await _userManager.GetUserAsync(User);
                currentUser.UserImagePath = path;
               await _userManager.UpdateAsync(currentUser);
                
                
                // UserImage userImage = new UserImage();
                // userImage.ImageFileName = userImageViewModel.ImageName;
                // await  _context.AddAsync(userImage);
                // await _context.SaveChangesAsync();
                return Redirect("https://localhost:5003/userphoto/photo"); 
        }
        
        
        
        // change the Email
        [HttpGet]
        public async Task<IActionResult> UserEmail()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEmail(int i)
        {
            return View();
        }
        
        
        
        // change the password
        [HttpGet]
        public async Task<IActionResult> UserPassword()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserPassword(int i)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            return View();
        }
    }
}