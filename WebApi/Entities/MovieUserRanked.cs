﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class MovieUserRanked
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        
        [Range(1, 5 ,ErrorMessage ="Value must be between {1} and {5}.")]
        public int Rank { get; set; }

    }
}