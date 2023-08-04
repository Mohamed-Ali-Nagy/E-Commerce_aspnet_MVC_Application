using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies_Store.Models;

namespace Movies_Store.Data
{
    public class CinemaContext:IdentityDbContext<ApplicationUser>
    {
        public CinemaContext(DbContextOptions options) : base(options) { }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actor_Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                  am.ActorId,
                  am.MovieId
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(m=>m.Movie).WithMany(am=>am.Actor_Movies).HasForeignKey(am=>am.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(a => a.Actor).WithMany(am => am.Actor_Movies).HasForeignKey(am => am.ActorId);     

            base.OnModelCreating(modelBuilder);
        }
    }
}
