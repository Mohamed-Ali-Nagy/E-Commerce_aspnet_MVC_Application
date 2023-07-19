using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies_Store.Data;
using Movies_Store.Models;

namespace Movies_Store.Controllers
{
    public class MovieController : Controller
    {
        private readonly CinemaContext context;
        public MovieController(CinemaContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> Index()
        {
            var actors =await context.Movies.Include(m=>m.Cinema).Include(m=>m.Producer).ToListAsync(); 
            return View(actors);
        }
    }
}
