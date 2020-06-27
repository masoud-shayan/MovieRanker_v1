using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace MVC.Models
{
    public class MovieModel
    {

        
        [Required]
        public string Name{ get; set; }
        
        [Required]
        public string Description{ get; set; }
        
        [Required]
        [Range(1800,2021, ErrorMessage ="Value must be between {1800} and {2021}.")]
        public int ReleaseDate{ get; set; }

        [Required] 
        public string Director{ get; set; }
        
        // [Required]
        [Range(1, 5 ,ErrorMessage ="Value must be between {1} and {5}.")]
        public double Rank{ get; set; }

        
                
        [Required(ErrorMessage = "Please Upload a Valid Image File.")]
        [Display(Name = "Upload User Image")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions ="jpg,png,gif,jpeg,bmp")]
        public IFormFile Image { get; set; }
        
        
        
        public string ImagePath{ get; set; }
        public double OverallRank { get; set; }
        public int RankCount { get; set; }
        
        // relations
        
        public Guid UserId { get; set; }
        
        
    }
}