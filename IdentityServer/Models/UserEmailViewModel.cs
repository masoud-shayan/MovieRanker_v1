using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class UserEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}