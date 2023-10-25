using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Helpers;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Areas.Admin.Controllers
{
    public class SliderItemController : Controller
    {
        private readonly ISliderItemService _sliderService;
        public SliderItemController(ISliderItemService sliderService) 
        {
            _sliderService = sliderService;
        }
        public async Task<IActionResult> Index()
        {
            var sliders = _sliderService.GetAllAsync();
            return View(sliders);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new SliderItem());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nazwa,Opis,IsActive")] SliderItem slider, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
               
                await _sliderService.AddSliderAsync(slider, image);
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var sliderItem = await _sliderService.GetByIdAsync(id.Value);
            if (sliderItem == null)
            {
                return NotFound();
            }

            return View(sliderItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SliderItemId,Nazwa,Opis,IsActive")] SliderItem slider, IFormFile? image)
        {
            if (id != slider.SliderItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    await _sliderService.UpdateSliderAsync(slider, image);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SliderExists(slider.SliderItemId))
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
            return View(slider);
        }

        private async Task<bool> SliderExists(int id)
        {
            return await _sliderService.GetByIdAsync(id) != null;
        }

    }
}
