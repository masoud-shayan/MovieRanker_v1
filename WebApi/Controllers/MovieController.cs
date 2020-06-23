using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
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

        public MovieController(ApplicationDbContext applicationDb , IWebHostEnvironment hostEnvironment)
        {
            _applicationDb = applicationDb;
            _hostEnvironment = hostEnvironment;
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
        public async Task<ActionResult<Movie>> GetMovie(Guid movieId)
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
        public async Task<IActionResult> AddMovie([FromForm] AddMovieViewModel movieModel )
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

            
            Console.WriteLine("userId   " + movieModel.UserId);


            

            // ------- create new movie with current movie data that coming from request body


            Movie movie = new Movie
            {
                Name = movieModel.Name,
                Description = movieModel.Description,
                ReleaseDate =  Int32.Parse(movieModel.ReleaseDate),
                Director = movieModel.Director,
                UserId = new Guid(movieModel.UserId),
                ImagePath = path ,
                RankCount = 0,
            };

            
            await _applicationDb.Movies.AddAsync(movie);
            // await _applicationDb.Movies.AddAsync(movie);
            await _applicationDb.SaveChangesAsync();
            
            Console.WriteLine(2);
            
            return CreatedAtAction(nameof(GetMovie), new {movieId = movie.Id}, movie);


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
            var newRank = movie.OverallRank + Math.Abs(rank - movie.OverallRank) / newCount;

            movie.RankCount = newCount;
            movie.OverallRank = newRank;

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


            var x =  _applicationDb.Users
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
    }
}