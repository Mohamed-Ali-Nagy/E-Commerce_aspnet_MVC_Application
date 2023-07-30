using Movies_Store.Data.Base;
using Movies_Store.Data.ViewModels;
using Movies_Store.Models;

namespace Movies_Store.Data.Services
{
    public interface IMovieService:IEntityBaseRepository<Movie>
    {
       Task< NewMovieDropdownsValuesVM> GetNewMovieDropdowns();
     //  Task<List<Movie>> FilterMovieAsync(string searchingValue);
       Task AddMovieAsync(NewMovieVM newMovie);
       Task<Movie> GetMovieByIdAsync(int id);
       Task UpdateMovieAsync(NewMovieVM movie);
    }
}
