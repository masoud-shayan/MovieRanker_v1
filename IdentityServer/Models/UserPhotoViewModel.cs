﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace IdentityServer.Models
{
    public class UserPhotoViewModel
    {
        
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        // [FileExtensions(Extensions ="jpg,png,gif,jpeg,bmp")]
        public IFormFile UserImageFile { get; set; }
    }
}