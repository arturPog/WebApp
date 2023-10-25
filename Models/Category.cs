using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Category
    {
        public Category() 
        {
            this.Products = new List<Product>();
        }
        [Key]
        public int CategoryId { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string NazwaUrl { get; set; }
        public string? Opis { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsMenu { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsActive { get; set; }
        public bool IsProduct { get; set; }
        public bool IsInfo { get; set; }
        public bool IsServis { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
