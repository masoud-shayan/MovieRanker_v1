﻿using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models
{
    public class User : IdentityUser
    {
        public string UserImagePath { get; set; }
    }
}