using Microsoft.AspNetCore.Mvc;
using Movies_Store.Data.Services;
using Movies_Store.Models;

namespace Movies_Store.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorService actorService;
        public ActorController(IActorService _actorService) 
        {
            actorService = _actorService;
        }
        public async Task<IActionResult> Index()
        {
            var actors= await actorService.GetAllAsync();
            return View(actors);
        }
        public  IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Actor newActor)
        {
             if(!ModelState.IsValid) 
            {
                return View(newActor);
            }
            await actorService.AddAsync(newActor);  
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(int id)
        {
            Actor actor=await actorService.GetByIdAsync(id);
            if(actor==null)
            {
                return View("NotFound");
            }
            return View(actor);

        }
        public async Task<IActionResult> Edit(int id)
        {
            Actor actor=await actorService.GetByIdAsync(id);
            if(actor==null)
            {
                return View("NotFound"); 
            }
            return View(actor);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await actorService.UpdateAsync(id, actor);
            return RedirectToAction("Index");
        }

        public async  Task<IActionResult> Delete(int id)
        {
            Actor actor = await actorService.GetByIdAsync(id);
            if (actor == null)
                return View("NotFound");
            await actorService.DeleteAsync(actor);

            return RedirectToAction("Index");
        }
    }
}
