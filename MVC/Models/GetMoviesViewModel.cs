using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class GetMoviesViewModel
    {
        [Key] 
        [Column("MovieId")] 
        [Required] 
        public Guid Id { get; set; }

        [Required] 
        public string Name { get; set; }

        [Required] 
        public string Description { get; set; }

        [Required]
        // [Column(TypeName="Date")]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        // [Range(2021,1800)]
        public int ReleaseDate { get; set; }

        [Required] 
        public string Director { get; set; }


        public double OverallRank { get; set; }

        [Required] public int RankCount { get; set; }


        [Required] public string ImagePath { get; set; }

        public DateTime CreatedDate { get; set; }


        // relations

        public Guid UserId { get; set; }
    }
}