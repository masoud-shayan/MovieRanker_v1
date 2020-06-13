﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace MVC.Models
{
    public class UserPhotoViewModel
    {
        // [Required]
        // public string ImageName { get; set; }
        
        [Required(ErrorMessage = "Please Upload a Valid Image File.")]
        [Display(Name = "Upload User Image")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions ="jpg,png,gif,jpeg,bmp")]
        public IFormFile UserImageFile { get; set; }
    }
}