using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Client
    {
        public Client() 
        {
            this.Orders = new List<Order>();
        }
        [Key]
        public int ClientId { get; set; }
        public string Nazwa { get; set; } = String.Empty;
        public ICollection<Order> Orders { get; set; }
    }
}
