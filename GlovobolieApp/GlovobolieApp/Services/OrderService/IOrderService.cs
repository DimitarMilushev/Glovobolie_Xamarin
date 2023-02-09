using GlovobolieApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GlovobolieApp.Services.OrderService
{
    interface IOrderService
    {
        Task<Order> GetOrderAsync(int id);
        Task<ICollection<Order>> GetAllOrdersAsync();
        Task PlaceOrderAsync(Order order, int userId);
    }
}
