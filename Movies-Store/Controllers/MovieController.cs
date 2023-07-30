using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies_Store.Data;
using Movies_Store.Data.Services;
using Movies_Store.Data.ViewModels;
using Movies_Store.Models;
using System.Linq.Expressions;

namespace Movies_Store.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        public MovieController(IMovieService _movieService)
        {
            movieService = _movieService;
        }
        public async Task<IActionResult> Index()
        {
           
            List<Movie> movies=await movieService.GetAllAsync(c=>c.Cinema);
            return View(movies);

        }
        public async Task<IActionResult>Details(int id)
        {
            Movie movie=await movieService.GetMovieByIdAsync(id);
            if (movie == null) return View("NotFound");
            return View(movie);
        }

        public async Task<IActionResult> Filter(string searchValue)
        {
            var allMovies = await movieService.GetAllAsync(m => m.Cinema);

            if (!searchValue.IsNullOrEmpty())
            {
                var searchingResult = allMovies.Where(m => m.Name.ToLower() == searchValue.ToLower() || m.Description.ToLower() == searchValue.ToLower()).ToList();
                return View("Index",searchingResult);

            }
            return View("Index",allMovies);
        }

        #region Add new movie
        public async Task<IActionResult> Add()
        {
            var movieDropdownData =await movieService.GetNewMovieDropdowns();
            ViewBag.Actors=new SelectList(movieDropdownData.Actors,"Id","Name");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "Name");
            ViewBag.Cinemas=new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(NewMovieVM newMovie)
        {
            if (!ModelState.IsValid) 
            {
                var movieDropdownData = await movieService.GetNewMovieDropdowns();
                ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "Name");
                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                return View(newMovie); 
            }
            await movieService.AddMovieAsync(newMovie);
            return RedirectToAction("Index");
        }
        #endregion

        #region Edit Movie
        public async Task<IActionResult> Edit(int id)
        {
            var movie=await movieService.GetMovieByIdAsync(id);
            if (movie == null) return View("NotFound");

            var movieVM = new NewMovieVM()
            {
                Id = movie.Id,
                Name = movie.Name,
                Price = movie.Price,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                Description = movie.Description,
                CinemaId = movie.CinemaId,
                ImageUrl = movie.ImageUrl,
                ProducerId = movie.ProducerId,
                MovieCategory = movie.MovieCategory,
                ActorsId= movie.Actor_Movies.Select(m=>m.ActorId).ToList(),
            };

         

            var movieDropdownData = await movieService.GetNewMovieDropdowns();
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "Name");
            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");

            return View(movieVM);

        }
        [HttpPost]
        public async Task<IActionResult>Edit(int id ,NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");

            var dbMovie=await movieService.GetMovieByIdAsync(id);
            if (movie == null) return View("NotFound");

            if(!ModelState.IsValid)
            {
                var movieDropdownData = await movieService.GetNewMovieDropdowns();
                ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "Name");
                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                return View(movie);
            }

            await movieService.UpdateMovieAsync(movie);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete movie
        //public async Task<IActionResult>Delete(int id)
        //{
        //    Movie movie=await movieService.GetMovieByIdAsync(id);
        //    if (movie == null) return View("NotFound");
        //    return View(movie);
        //}
        #endregion
    }
}
