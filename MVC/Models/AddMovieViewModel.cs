using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC.Models
{
    public class AddMovieViewModel
    {
        // [Required]
        public string Name{ get; set; }
        
        // [Required]
        public string Description{ get; set; }
        
        // [Required]
        // [Range(1800,2021, ErrorMessage ="Value must be between {1800} and {2021}.")]
        public int ReleaseDate{ get; set; }

        // [Required] 
        public string Director{ get; set; }
        
                
        [Required(ErrorMessage = "Please Upload a Valid Image File.")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }


    }
}