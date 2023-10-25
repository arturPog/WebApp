using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class SliderItem
    {
        [Key]
        public int SliderItemId { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
