using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class MovieModel
    {

        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string Name{ get; set; }
        
        [Required]
        public string Description{ get; set; }
        
        [Required]
        [Range(2021,1800, ErrorMessage ="Value must be between {1800} and {2021}.")]
        public int ReleaseDate{ get; set; }

        [Required] 
        public string Director{ get; set; }
        
        [Required]
        [Range(1, 5 ,ErrorMessage ="Value must be between {1} and {5}.")]
        public double Rank{ get; set; }

        [Required]
        public string ImagePath{ get; set; }
        
        // relations
        
        public Guid UserId { get; set; }


    }
}