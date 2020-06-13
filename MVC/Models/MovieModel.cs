using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace MVC.Models
{
    public class MovieModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public string Director { get; set; }
        public IFormFile Image { get; set; }
        public int Rank { get; set; }
    }
}