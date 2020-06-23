using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class AddUserViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
        

        public string Email { get; set; }

        public string UserImagePath { get; set; }
    }
}