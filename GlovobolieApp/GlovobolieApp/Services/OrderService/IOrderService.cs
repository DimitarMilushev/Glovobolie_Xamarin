using GlovobolieApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GlovobolieApp.Services.OrderService
{
    interface IOrderService
    {
        Task<ICollection<Order>> GetAllOrdersByUserAsync(int userId);
        Task PlaceOrderAsync(Order order);
    }
}
