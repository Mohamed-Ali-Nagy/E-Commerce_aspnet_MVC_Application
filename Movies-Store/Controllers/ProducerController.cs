using Microsoft.AspNetCore.Mvc;
using Movies_Store.Data.Services;
using Movies_Store.Models;

namespace Movies_Store.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IProducerService producerService;
        public ProducerController(IProducerService _producerService) 
        {
            producerService = _producerService;
        }
        public async Task<IActionResult> Index()
        {
          var producers= await producerService.GetAllAsync();
            return View(producers);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Producer producer)
        {
            if(!ModelState.IsValid)
            {
                return View(producer);
            };
            await producerService.AddAsync(producer);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            Producer producer=await producerService.GetByIdAsync(id);
            if (producer == null) { return View("NotFound"); }
            return View(producer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (!ModelState.IsValid) { return View(producer); }
            if (id != producer.Id) { return View("NotFound"); }
            await producerService.UpdateAsync(id, producer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Producer producer=await producerService.GetByIdAsync(id);
            if (producer == null) { return View("NotFound"); }
            return View(producer);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Producer producerDetails=await producerService.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await producerService.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");

            await producerService.DeleteAsync(producerDetails);
            return RedirectToAction(nameof(Index));
        }


    }
}
