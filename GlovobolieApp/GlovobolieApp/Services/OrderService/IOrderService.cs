using GlovobolieApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GlovobolieApp.Services.OrderService
{
    interface IOrderService
    {
        Task<Order> GetOrder(string id);
        Task<ICollection<Order>> GetAllOrders();
        Task PlaceOrder(Order order, string userId);
    }
}
