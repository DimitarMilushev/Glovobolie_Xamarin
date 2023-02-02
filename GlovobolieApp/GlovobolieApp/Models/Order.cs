using GlovobolieApp.Models.Enums;
using System;
using System.Collections.Generic;

namespace GlovobolieApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public ICollection<Product> Products { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
    }
}
