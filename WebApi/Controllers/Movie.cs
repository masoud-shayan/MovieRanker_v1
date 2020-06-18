using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Movie : Controller
    {
        private readonly ApplicationDbContext _applicationDb;

        public Movie(ApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<Entities.Movie>>> GetMovies()
        {
            List<Entities.Movie> allMovies = await _applicationDb.Movies
                .OrderBy(m => m.CreatedDate).ToListAsync();

            return allMovies;
        }

        [HttpGet("{movieId}")]
        [Authorize]
        public async Task<ActionResult<Entities.Movie>> GetMovie(Guid movieId)
        {
            var movie = await _applicationDb.Movies.FindAsync(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
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
        public async Task<IActionResult> AddMovie(WebApi.Entities.MovieModel movieModel)
        {
            Entities.Movie movie = new Entities.Movie()
            {
                Name = movieModel.Name,
                Description = movieModel.Description,
                ReleaseDate = movieModel.ReleaseDate,
                Director = movieModel.Director,
                Rank = movieModel.Rank,
                ImagePath = movieModel.ImagePath,
                RankCount = 1
            };

            await _applicationDb.Movies.AddAsync(movie);
            await _applicationDb.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new {id = movie.Id}, movie);
        }


        [HttpPut("{movieId}")]
        [Authorize]
        public async Task<IActionResult> UpdateMovieRank(Guid movieId, int rank)
        {
            // if (id != todoItem.Id)
            // {
            //     return BadRequest();
            // }

            var movie = await _applicationDb.Movies.FindAsync(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            var newCount = movie.RankCount + 1;
            var newRank = movie.Rank + Math.Abs(rank - movie.Rank) / newCount;

            movie.RankCount = newCount;
            movie.Rank = newRank;

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
            
            var extractedUser = await _applicationDb.Users.Include(u => u.Movies).FirstOrDefaultAsync(u => u.Id == userId);


            ActionResult<IList<Entities.Movie>> movies = extractedUser.Movies.ToList();


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

            ActionResult<IList<Entities.Movie>> movies = await _applicationDb.Users
                .Include(item => item.MoviesUsersRanked)
                .ThenInclude(item => item.Movie).Where(u => u.Id == userId);


            return movies;
        }
    }
}