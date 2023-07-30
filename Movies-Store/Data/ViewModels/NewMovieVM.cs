//using Microsoft.Build.Framework;
using Movies_Store.Data.Enums;
using Movies_Store.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies_Store.Data.ViewModels
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Display(Name = "Movie Name")]
        [Required(ErrorMessage = "Movie Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage="Description is required")]
        public string Description { get; set; }
        [Display(Name = "Movie Poster")]
        [Required(ErrorMessage = "ImageURL is required")]
        public string ImageUrl { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [Display(Name = "Movie Start Date")]
        [Required(ErrorMessage = "Movie start date is required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Movie End date")]
        [Required(ErrorMessage = "Movie End date is required")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Movie Category")]
        [Required(ErrorMessage = "Movie Category is required")]
        public MoviesCategory MovieCategory { get; set; }
        [Display(Name = "Cinema")]
        [Required(ErrorMessage = "Cinema is required")]
        public int CinemaId { get; set; }
        [Display(Name = "Movie Producer")]
        [Required(ErrorMessage = "Movie Producer is required")]
        public int ProducerId { get; set; }
        [Display(Name = "Actors")]
        [Required(ErrorMessage = "Movie actors is required")]
        public List<int> ActorsId { get; set; }= new List<int>();

    }
}
