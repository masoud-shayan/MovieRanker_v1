using System.Buffers.Text;

namespace WebApi.Entities
{
    public class RetrieveMovieViewModel : Movie
    {
        public int UserRank { get; set; }

        public RetrieveMovieViewModel(Movie movie)
        {
            this.Id = movie.Id;
            this.Name = movie.Name;
            this.Description = movie.Description;
            this.Director = movie.Director;
            this.ReleaseDate = movie.ReleaseDate;
            this.CreatedDate = movie.CreatedDate;
            this.UserId = movie.UserId;
            this.OverallRank = movie.OverallRank;
            this.ImagePath = movie.ImagePath;
            this.RankCount = movie.RankCount;
            this.UserRank = 0;
        }

    }
}