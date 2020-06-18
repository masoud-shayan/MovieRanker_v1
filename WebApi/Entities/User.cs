using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Entities
{
    public class User 
    {
        [Key]
        [Column("UserId")]
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        public string UserImage { get; set; }

        
        
        
        // relations
        
        public ICollection<Movie> Movies { get; set; }
        
        // public Guid MovieId { get; set; }
        // public Movie Movie { get; set; }
        
        public ICollection<MovieUserRanked> MoviesUsersRanked { get; set; }

    }
}