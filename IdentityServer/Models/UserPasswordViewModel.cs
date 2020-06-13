using System.ComponentModel.DataAnnotations;

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
        public string NewPassword { get; set; }
    }
}