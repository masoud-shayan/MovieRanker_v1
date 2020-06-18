using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Movie
    {
        [Key]
        [Column("MovieId")]
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string Name{ get; set; }
        
        [Required]
        public string Description{ get; set; }
        
        [Required]
        // [Column(TypeName="Date")]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        [Range(2021,1800)]
        public int ReleaseDate{ get; set; }

        [Required] 
        public string Director{ get; set; }
        
        [Required]
        [Range(1, 5 ,ErrorMessage ="Value must be between {1} and {5}.")]
        public double Rank{ get; set; }
        
        [Required]
        public int RankCount{ get; set; }
        
        
        [Required]
        public string ImagePath{ get; set; } 
        
        public DateTime CreatedDate{ get; set; }
        


        // relations
        
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        
        public ICollection<MovieUserRanked> MoviesUsersRanked { get; set; }



    }
}