﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public Order Order { get; set; }
    }
}
