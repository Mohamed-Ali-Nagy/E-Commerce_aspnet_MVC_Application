using Movies_Store.Models;

namespace Movies_Store.Data.ViewModels
{
    public class NewMovieDropdownsValuesVM
    {
        public List<Actor> Actors { get; set; } = new List<Actor>();
        public List<Producer> Producers { get; set;}=new List<Producer>();
        public List<Cinema> Cinemas { get; set;}=new List<Cinema>();
    }
}
