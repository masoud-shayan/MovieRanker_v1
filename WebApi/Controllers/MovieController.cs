using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MVC.Models;
using Newtonsoft.Json;
using WebApi.Data;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MovieController(ApplicationDbContext applicationDb, IWebHostEnvironment hostEnvironment)
        {
            _applicationDb = applicationDb;
            _hostEnvironment = hostEnvironment;
        }


        [HttpGet]
        // [Authorize]
        public async Task<ActionResult<IList<Movie>>> GetMovies()
        {
            List<Movie> allMovies = await _applicationDb.Movies
                .OrderByDescending(m => m.CreatedDate).ToListAsync();

            return allMovies;
        }

        [HttpGet("{movieName}")]
        // [Authorize]
        public async Task<ActionResult<RetrieveMovieViewModel>> GetMovie(string movieName)
        {
            // -------- take access token from current user context
            var userId = await TakeUserIdByAccessToken();

            if (userId.Equals("Unauthorized"))
            {
                return Unauthorized();
            }


            Movie movie = await _applicationDb.Movies.SingleOrDefaultAsync(m => m.Name == movieName);

            if (movie == null)
            {
                return NotFound();
            }

            // ------- assign movie properties value to RetrieveMovieViewModel properties value
            var retrieveMovie = new RetrieveMovieViewModel(movie);


            var movieUserRanked = await _applicationDb.MovieUserRankeds
                .SingleOrDefaultAsync(m => m.UserId == new Guid(userId) && m.MovieId == movie.Id);


            if (movieUserRanked != null)
            {
                retrieveMovie.UserRank = movieUserRanked.Rank;
            }

            return retrieveMovie;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<Entities.Movie>>> GetRecentAddedMovies()
        {
            List<Entities.Movie> lastRecent = await _applicationDb.Movies
                .OrderByDescending(m => m.CreatedDate)
                .Take(10).ToListAsync();

            return lastRecent;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddMovie([FromForm] AddMovieViewModel movieModel)
        {
            // ------- save ImageFile to directory
            var rootPath = _hostEnvironment.WebRootPath;
            var fileName = Path.GetFileNameWithoutExtension(movieModel.ImageFile.FileName);
            var extension = Path.GetExtension(movieModel.ImageFile.FileName);
            var userPhotoName = fileName + DateTime.Now.ToString("yymmddssfff") + extension;
            string path = Path.Combine(rootPath, "image", userPhotoName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await movieModel.ImageFile.CopyToAsync(fileStream);
            }


            // ------- create new movie with current movie data that coming from request body
            Movie movie = new Movie
            {
                Name = movieModel.Name,
                Description = movieModel.Description,
                ReleaseDate = Int32.Parse(movieModel.ReleaseDate),
                Director = movieModel.Director,
                UserId = new Guid(movieModel.UserId),
                ImagePath = path,
                RankCount = 0,
            };


            await _applicationDb.Movies.AddAsync(movie);
            // await _applicationDb.Movies.AddAsync(movie);
            await _applicationDb.SaveChangesAsync();

            var retrieveMovie = new RetrieveMovieViewModel(movie);


            return CreatedAtAction(nameof(GetMovie), new {movieName = retrieveMovie.Name},
                retrieveMovie);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RankMovie(MovieRankModel rankModel)
        {
            // -------- take access token from current user context
            var userId = await TakeUserIdByAccessToken();

            if (userId.Equals("Unauthorized"))
            {
                return Unauthorized();
            }


            // ------- search to get the movie with provided movie's name
            Movie movie = await _applicationDb.Movies.SingleOrDefaultAsync(m => m.Name == rankModel.MovieName);

            if (movie == null)
            {
                return NotFound();
            }

            // ------- search to get the MovieUserRanked record with same movie id and user id  !
            MovieUserRanked rankRecord = await _applicationDb.MovieUserRankeds
                .SingleOrDefaultAsync(mur => mur.UserId == new Guid(userId) && mur.MovieId == movie.Id);


            // --- if the MovieUserRanked record exists then there is no new rank count, otherwise we have and we should accumulate it 
            int newCount = 0;
            double newOverallRank = 0;


            // ------- if not ? then create a new one
            if (rankRecord != null)
            {
                newCount = movie.RankCount;

                newOverallRank = UpdateOverallRank(movie.OverallRank, movie.RankCount, rankRecord.Rank,
                    rankModel.MovieRank);


                rankRecord.Rank = rankModel.MovieRank;
                _applicationDb.MovieUserRankeds.Update(rankRecord);
            }
            else
            {
                newCount = movie.RankCount + 1;

                newOverallRank = AddToOverallRank(movie.OverallRank, movie.RankCount, rankModel.MovieRank);

                rankRecord = new MovieUserRanked
                {
                    UserId = new Guid(userId),
                    MovieId = movie.Id,
                    Rank = rankModel.MovieRank
                };
                await _applicationDb.MovieUserRankeds.AddAsync(rankRecord);
            }


            movie.RankCount = newCount;
            movie.OverallRank = Math.Round(newOverallRank, 2);

            _applicationDb.Entry(movie).State = EntityState.Modified;


            try
            {
                await _applicationDb.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{movieName}")]
        [Authorize]
        public async Task<IActionResult> DeleteMovie(string movieName)
        {
            var userId = await TakeUserIdByAccessToken();

            if (userId.Equals("Unauthorized"))
            {
                return Unauthorized();
            }


            var movie = await _applicationDb.Movies.SingleOrDefaultAsync(m => m.Name == movieName);
            if (movie == null)
            {
                return NotFound();
            }

            if (movie.UserId == new Guid(userId))
            {
                _applicationDb.Movies.Remove(movie);
                var rankRecords = await _applicationDb.MovieUserRankeds
                    .Where(mur => mur.UserId == new Guid(userId) && mur.MovieId == movie.Id).ToListAsync();

                if (rankRecords != null)
                {
                    _applicationDb.MovieUserRankeds.RemoveRange(rankRecords);
                }
                
                await _applicationDb.SaveChangesAsync();
            }
            else
            {
                return Unauthorized();
            }


            return NoContent();
        }

        [HttpGet("{movieId}")]
        [Authorize]
        public async Task<ActionResult<Entities.User>> MovieAddedBy(Guid movieId)
        {
            var movie = await _applicationDb.Movies.FindAsync(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            return movie.User;
        }


        [HttpGet("{movieId}")]
        [Authorize]
        public async Task<ActionResult<IList<Entities.User>>> MovieRankedBy(Guid movieId)
        {
            var movie = await _applicationDb.Movies.FindAsync(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            ActionResult<IList<Entities.User>> users = await _applicationDb.Movies
                .Include(item => item.MoviesUsersRanked)
                .ThenInclude(item => item.User).Where(m => m.Id == movieId).Select(m => m.User).ToListAsync();


            return users;
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<ActionResult<IList<Entities.Movie>>> AddedMoviesByUser(Guid userId)
        {
            var user = await _applicationDb.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }


            // var extractedUser = await _applicationDb.Users.Include(u => u.Movies).FirstOrDefaultAsync(u => u.Id == userId);
            // ActionResult<IList<Entities.Movie>> movies = extractedUser.Movies.ToList();


            var movies = user.Movies.ToList();


            return movies;
        }


        [HttpGet("{userId}")]
        [Authorize]
        public async Task<ActionResult<IList<Entities.Movie>>> RankedMoviesByUser(Guid userId)
        {
            var user = await _applicationDb.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }


            var x = _applicationDb.Users
                .Where(u => u.Id == userId)
                .Include(u => u.MoviesUsersRanked)
                .ThenInclude(mur => mur.Movie);


            foreach (var u in x)
            {
            }

            var movies = user.Movies.ToList();

            return movies;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> GetUserId(AddUserViewModel incomingUser)
        {
            var user = await _applicationDb.Users.FindAsync(new Guid(incomingUser.UserId));

            if (user == null)
            {
                user = new User()
                {
                    Id = new Guid(incomingUser.UserId),
                    UserName = incomingUser.UserName,
                    UserImage = incomingUser.UserImagePath
                };

                await _applicationDb.Users.AddAsync(user);
                await _applicationDb.SaveChangesAsync();
            }

            return user.Id;
        }

        private double UpdateOverallRank(double currentOverallRank, int currentRankCount, int previousRank, int newRank)
        {
            double Average_OverallRank_Before_PreviousValue = 0;
            double newOverallRank = 0;

            if (currentRankCount == 1)
            {
                newOverallRank = newRank;
            }
            else
            {
                // ------ 1. fix the previous Average by extracting previous rank from the previous Averaging by
                // ------ this formula : Average Rank before previous value = Current Average Rank + (Current Average Rank - previous rank )/(Current Rank Count -1)
                Average_OverallRank_Before_PreviousValue =
                    currentOverallRank + (currentOverallRank - previousRank) / (currentRankCount - 1);

                Console.WriteLine("deleting previous " + Average_OverallRank_Before_PreviousValue);

                // ------- 2 . add the new rank to the Average Rank before previous value to calculate new Average Rank
                // ------- by this formula : new Average Rank =  Current Average Rank + (new Rank - Current Average Rank)/(Current Rank Count + 1)
                newOverallRank =
                    Average_OverallRank_Before_PreviousValue +
                    (newRank - Average_OverallRank_Before_PreviousValue) / (currentRankCount);
            }


            return newOverallRank;
        }

        private double AddToOverallRank(double currentOverallRank, int currentRankCount, int newRank)
        {
            double newOverallRank =
                currentOverallRank +
                (newRank - currentOverallRank) / (currentRankCount + 1);


            return newOverallRank;
        }

        private async Task<string> TakeUserIdByAccessToken()
        {
            var access_token = await HttpContext.GetTokenAsync("access_token");

            string userId = "";

            if (string.IsNullOrEmpty(access_token))
            {
                userId = "Unauthorized";
            }

            // ------- Extract userId (sub) from access token
            string accessTokenString = new JwtSecurityTokenHandler().ReadJwtToken(access_token).ToString();
            string toBeSearched = "\"sub\":\"";
            userId = accessTokenString.Substring(accessTokenString.IndexOf(toBeSearched) + toBeSearched.Length);
            userId = userId.Substring(0, userId.IndexOf("\""));

            return userId;
        }
    }
}