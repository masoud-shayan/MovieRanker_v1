using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            
            // ------- Movie - User Relations
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.User)
                .WithMany(u => u.Movies)
                .HasForeignKey(m => m.UserId);
            
            
            
            // ------- Movie , User ======> MovieUserRanked Relations

            
            // ------- composite primary key
            modelBuilder.Entity<MovieUserRanked>()
                .HasKey(t => new { t.UserId, t.MovieId});

            modelBuilder.Entity<MovieUserRanked>()
                .HasOne(mur => mur.User)
                .WithMany(u => u.MoviesUsersRanked)
                .HasForeignKey(mur => mur.UserId);

            modelBuilder.Entity<MovieUserRanked>()
                .HasOne(mur => mur.Movie)
                .WithMany(m => m.MoviesUsersRanked)
                .HasForeignKey(mur => mur.MovieId);
            
            modelBuilder.Entity<MovieUserRanked>()
                .HasOne(mur => mur.Movie)
                .WithMany(m => m.MoviesUsersRanked)
                .HasForeignKey(mur => mur.MovieId);
            
            
            
            // ------- Auto Generate Date for CreatedDate
            modelBuilder.Entity<Movie>()
                .Property(m => m.CreatedDate )
                .HasDefaultValueSql("CURRENT_DATE"); // just for postgres
            
            
        }
    }
}