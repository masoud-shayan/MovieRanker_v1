﻿using System;
using System.IO;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IIdentityServerInteractionService _interactionService;


        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager,
            IWebHostEnvironment hostEnvironment , IIdentityServerInteractionService interactionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnvironment = hostEnvironment;
            _interactionService = interactionService;
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
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var result =
                await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, true, false);

            if (result.Succeeded)
            {
                // return Redirect(returnUrl);

                if (returnUrl.IsNullOrEmpty())
                {
                    return Redirect("https://localhost:5003/Home/UserIndex");
                }

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
            if (result.RequiresTwoFactor)
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
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            // var userNormalizer = registerViewModel.Email.Substring(0, registerViewModel.Email.IndexOf("@"));

            var user = new User()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (!result.Succeeded)
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

            if (returnUrl.IsNullOrEmpty())
            {
                return Redirect("https://localhost:5003/Home/UserIndex");
            }

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

            var applicationUser = await _userManager.GetUserAsync(User);
            var userPhoto = applicationUser?.UserImagePath;

            if (!string.IsNullOrEmpty(userPhoto))
            {
                string toBeSearched = "wwwroot";
                string imagePathNormalizer = userPhoto.Substring(userPhoto.IndexOf(toBeSearched) + toBeSearched.Length);
                ViewBag.UserImagePath = imagePathNormalizer;
            }
            else
            {
                ViewBag.UserImagePath = "";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserPhoto(UserPhotoViewModel userPhotoViewModel)
        {

            // -------- image file validations
            if (!ModelState.IsValid)
            {
                return View(userPhotoViewModel);
            }
            
            var ext = Path.GetExtension(userPhotoViewModel.UserImageFile.FileName);
            
            if (!ext.Equals(".jpg") && !ext.Equals(".png") && !ext.Equals(".jpeg") && !ext.Equals(".bmp") )
            {
                ModelState.TryAddModelError("", "The Image File  only accepts files with the following extensions: .jpg, .png, .jpeg, .bmp");
            
                return View(userPhotoViewModel);
            }



            // save image to root/image
            var rootPath = _hostEnvironment.WebRootPath;
            var fileName = Path.GetFileNameWithoutExtension(userPhotoViewModel.UserImageFile.FileName);
            var extension = Path.GetExtension(userPhotoViewModel.UserImageFile.FileName);
            var userPhotoName = fileName + DateTime.Now.ToString("yymmddssfff") + extension;
            string path = Path.Combine(rootPath, "image", userPhotoName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await userPhotoViewModel.UserImageFile.CopyToAsync(fileStream);
            }


            var currentUser = await _userManager.GetUserAsync(User);


            // ------- delete previous image from root/image if exists
            var previousImage = currentUser.UserImagePath;
            if (!string.IsNullOrEmpty(previousImage))
            {
                if (System.IO.File.Exists(previousImage))
                {
                    System.IO.File.Delete(previousImage);
                }
            }


            // ------- save image path in database
            // var currentUser = await _userManager.GetUserAsync(User);
            currentUser.UserImagePath = path;
            await _userManager.UpdateAsync(currentUser);


            // UserImage userImage = new UserImage();
            // userImage.ImageFileName = userImageViewModel.ImageName;
            // await  _context.AddAsync(userImage);
            // await _context.SaveChangesAsync();
            return Redirect("https://localhost:5003/User/Change_Settings");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserPhoto()
        {
            // ------- find User Image Path 
            var currentUser = await _userManager.GetUserAsync(User);
            var currentImagePath = currentUser?.UserImagePath;

            // ------- delete it from file system
            if (!string.IsNullOrEmpty(currentImagePath))
            {
                if (System.IO.File.Exists(currentImagePath))
                {
                    System.IO.File.Delete(currentImagePath);
                }
            }

            // ------ delete if from Database 

            currentUser.UserImagePath = "";
            await _userManager.UpdateAsync(currentUser);

            return Redirect("https://localhost:5003/User/Change_Settings");
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

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();

            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
            {
                return Redirect("https://localhost:5003/Home/Index");
            }
            
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}