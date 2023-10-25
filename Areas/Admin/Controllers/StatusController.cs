using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Areas.Admin.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService) 
        {
            _statusService = statusService;
        }

        public async Task<IActionResult> Index()
        {
            var status = _statusService.GetAllAsync();
            return View(status);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new SliderItem());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nazwa,Opis")] Status status)
        {
            if (ModelState.IsValid)
            {

                await _statusService.AddStatusAsync(status);
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var sliderItem = await _statusService.GetByIdAsync(id.Value);
            if (sliderItem == null)
            {
                return NotFound();
            }

            return View(sliderItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusId,Nazwa,Opis")] Status status)
        {
            if (id != status.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _statusService.UpdateStatusAsync(status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await StatusExists(status.StatusId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Zakładając, że istnieje akcja Index
            }
            return View(status);
        }

        private async Task<bool> StatusExists(int id)
        {
            return await _statusService.GetByIdAsync(id) != null;
        }
    }
}
