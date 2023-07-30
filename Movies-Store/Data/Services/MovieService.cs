using Microsoft.EntityFrameworkCore;
using Movies_Store.Data.Base;
using Movies_Store.Data.ViewModels;
using Movies_Store.Models;

namespace Movies_Store.Data.Services
{
    public class MovieService:EntityBaseRepository<Movie>,IMovieService
    {
        private readonly CinemaContext context;
        public MovieService(CinemaContext _context):base(_context) 
        {
            context = _context;
        }
        //public async Task<List<Movie>> FilterMovieAsync(string searchingValue)
        //{
        //   return await context.Movies.Where(m=>m.Name.ToLower()==searchingValue.ToLower()||m.Description.ToLower() ==searchingValue.ToLower()).ToListAsync();
            
        //}

        public async Task AddMovieAsync(NewMovieVM newMovie)
        {
            Movie movie=new Movie()
            {
                Name = newMovie.Name,
                Price = newMovie.Price,
                StartDate = newMovie.StartDate,
                EndDate = newMovie.EndDate,
                Description = newMovie.Description,
                CinemaId = newMovie.CinemaId,
                ImageUrl = newMovie.ImageUrl,
                ProducerId = newMovie.ProducerId,
                MovieCategory=newMovie.MovieCategory,

            };
            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();

            foreach(var actor in newMovie.ActorsId)
            {
                var actorMovie = new Actor_Movie()
                {
                    ActorId = actor,
                    MovieId = movie.Id,
                };
                await context.Actor_Movies.AddAsync(actorMovie);
            }
            await context.SaveChangesAsync();


        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            Movie movie= await context.Movies.Where(m => m.Id == id)
               .Include(c => c.Cinema)
               .Include(p => p.Producer)
               .Include(n => n.Actor_Movies)
               .ThenInclude(a => a.Actor).FirstOrDefaultAsync();

          

            return movie;
        }

        public async Task< NewMovieDropdownsValuesVM> GetNewMovieDropdowns()
        {
            var result=new NewMovieDropdownsValuesVM()
            {
                Actors= await context.Actors.OrderBy(a=>a.Name).ToListAsync(),
                Cinemas=await context.Cinemas.OrderBy(c=>c.Name).ToListAsync(),
                Producers=await context.Producers.OrderBy(p=>p.Name).ToListAsync(),
            };
            return result;

        }

        public async Task UpdateMovieAsync(NewMovieVM movieVM)
        {
           

            Movie movie =await context.Movies.Where(m=>m.Id== movieVM.Id).FirstOrDefaultAsync();


            movie.Name = movieVM.Name;
            movie.Price = movieVM.Price;
            movie.StartDate = movieVM.StartDate;
            movie.EndDate = movieVM.EndDate;
            movie.Description = movieVM.Description;
            movie.CinemaId = movieVM.CinemaId;
            movie.ImageUrl = movieVM.ImageUrl;
            movie.ProducerId = movieVM.ProducerId;
            movie.MovieCategory = movieVM.MovieCategory;

            
            //context.Movies.Update(movie);
            await context.SaveChangesAsync();
            //remove old actors
            var oldActorsList=await context.Actor_Movies.Where(am=>am.MovieId==movieVM.Id).ToListAsync();
            context.Actor_Movies.RemoveRange(oldActorsList);
            await context.SaveChangesAsync();

            foreach (var actor in movieVM.ActorsId)
            {
                var actorMovie = new Actor_Movie()
                {
                    ActorId = actor,
                    MovieId = movie.Id,
                };
                await context.Actor_Movies.AddAsync(actorMovie);
            }
            await context.SaveChangesAsync();
        }
    }
}
