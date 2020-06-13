﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class UserPhoto
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string ImageFileName { get; set; }
        
        
    }
}