using System.ComponentModel.DataAnnotations;
using IdentityServer.Validators;

namespace IdentityServer.Models
{
    public class UserPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        
        [Required]
        [DataType(DataType.Password)]
        // [Compare("Password", ErrorMessage = "the Password and the NewPassword must be the same.")]
        [NotEqual("Password")]
        public string NewPassword { get; set; }
    }
}