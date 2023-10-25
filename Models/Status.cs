using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string Nazwa { get; set; } = String.Empty;
        public string Opis { get; set; } = String.Empty;
    }
}
