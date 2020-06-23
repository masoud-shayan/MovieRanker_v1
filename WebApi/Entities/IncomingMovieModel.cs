using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class IncomingMovieModel
    {

        public Guid Id { get; set; }
        
        public string Name{ get; set; }
        
        public string Description{ get; set; }
        
        [Range(2021,1800, ErrorMessage ="Value must be between {1800} and {2021}.")]
        public int ReleaseDate{ get; set; }

        public string Director{ get; set; }
        
        [Range(1, 5 ,ErrorMessage ="Value must be between {1} and {5}.")]
        public double Rank{ get; set; }

        public string ImagePath{ get; set; }
        
        // relations
        
        public Guid UserId { get; set; }
        
    }
}