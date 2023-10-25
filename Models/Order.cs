using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Order
    {
        public Order() 
        {
            this.OrderDetails = new List<OrderDetail>();
        }
        [Key]
        public int OrderId { get; set; }
        public DateTime DataZamówienia { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
