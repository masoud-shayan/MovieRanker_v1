using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class UserInfoViewModel
    {
        

        public Guid UserId { get; set; }
        
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string UserImagePath { get; set; }
    }
}