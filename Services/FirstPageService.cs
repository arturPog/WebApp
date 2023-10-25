using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class FirstPageService : IFirstPageService
    {
        private readonly AppDbContext _context;

        public FirstPageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PageViewModel> GetFirstPageViewModelAsync()
        {
            var viewModel = new PageViewModel
            {
                SliderItems = await _context.SliderItems.Where(x => x.IsActive).ToListAsync(),
                FirstPageCategories = await _context.Categories.Where(k => k.IsFirstPage).ToListAsync(),
                FirstPageProducts = await _context.Products.Where(p => p.IsFirstPage).ToListAsync()
            };

            return viewModel;
        }
    }
}
