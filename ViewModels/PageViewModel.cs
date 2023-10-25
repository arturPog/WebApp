using WebApp.Models;

namespace WebApp.ViewModels
{
    public class PageViewModel
    {
        
        public IEnumerable<SliderItem> SliderItems { get; set; }
        public IEnumerable<Category> FirstPageCategories { get; set; }
        public IEnumerable<Category> Menu { get; set; }
        public Category CurrentCategory { get; set; }
        public IEnumerable<Product> FirstPageProducts { get; set; }
        public IEnumerable<Product> ProductsList { get; set; }
        public Product CurrentProduct { get; set; }
        public string CategoryNameUrl { get; set; }
        public int CategoryId { get; set; }
        public string ProductNameUrl { get; set; }
        public int ProductId { get; set; }
    }
}
