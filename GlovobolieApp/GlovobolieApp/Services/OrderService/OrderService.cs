using GlovobolieApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GlovobolieApp.Services.OrderService
{
    public class OrderService : DataServiceBase, IOrderService
    {
        public Task<ICollection<Order>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrder(string id)
        {
            throw new NotImplementedException();
        }

        public Task PlaceOrder(Order order, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
