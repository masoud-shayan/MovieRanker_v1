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
        // [Range(2021,1800, ErrorMessage ="Value must be between {1800} and {2021}.")]
        public string ReleaseDate{ get; set; }

        // [Required] 
        public string Director{ get; set; }
        
                
        // [Required(ErrorMessage = "Please Upload a Valid Image File.")]
        // [Display(Name = "Upload User Image")]
        // [DataType(DataType.Upload)]
        // [FileExtensions(Extensions ="jpg,png,gif,jpeg,bmp")]
        public IFormFile ImageFile { get; set; }

        
        // relations
        
        public string UserId { get; set; }

    }
}