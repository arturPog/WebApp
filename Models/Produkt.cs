using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Product
    {
        [Key]
        public int ProduktId { get; set; }
        public string Nazwa { get; set; }
        public string NazwaUrl { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsFirstPage { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual Category Category { get; set; }
    }
}
