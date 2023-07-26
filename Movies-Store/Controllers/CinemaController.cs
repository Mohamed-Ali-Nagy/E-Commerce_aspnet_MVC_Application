using Microsoft.AspNetCore.Mvc;
using Movies_Store.Data.Services;
using Movies_Store.Models;

namespace Movies_Store.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ICinemaService cinemaService;
        public CinemaController(ICinemaService _cinemaService)
        {
            cinemaService = _cinemaService;
        }
        public async Task< IActionResult> Index()
        {
            var cinemas=await cinemaService.GetAllAsync();

            return View(cinemas);
        }
        public async Task<IActionResult> Details(int id)
        {
            var cinema=await cinemaService.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");

            return View(cinema);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Cinema cinema)
        {
            if (!ModelState.IsValid) { return View(cinema); }
            await cinemaService.AddAsync(cinema);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
           var cinema=await cinemaService.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Cinema cinema)
        {
            if (!ModelState.IsValid) { return View(cinema); } 
            if (cinema.Id != id) return View("NotFound");
            await cinemaService.UpdateAsync(id, cinema);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult>Delete(int id)
        {
            var cinema=await cinemaService.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var cinema = await cinemaService.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");

            await cinemaService.DeleteAsync(cinema);
            return RedirectToAction("Index");
        }
    }
}
