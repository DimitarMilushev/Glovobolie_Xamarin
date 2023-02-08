using GlovobolieApp.Models;
using GlovobolieApp.Models.Enums;
using GlovobolieApp.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.Services.OrderService
{
    public class OrderServiceMock : DataServiceBase, IOrderService
    {
        Random random = new Random();
        public async Task<ICollection<Order>> GetAllOrders()
        {
            Thread.Sleep(1000);
            return new List<Order>
            {
                new Order { Id = 1, Date= DateTime.Now, Products = await this.GetRandomProducts(), Status = this.GetRandomStatus(), UserId = 1 },
                new Order { Id = 2, Date= DateTime.Today, Products = await this.GetRandomProducts(), Status = this.GetRandomStatus(), UserId = 1 },
                new Order { Id = 3, Date= DateTime.Now, Products = await this.GetRandomProducts(), Status = this.GetRandomStatus(), UserId = 1 },
                new Order { Id = 4, Date= DateTime.Today, Products = await this.GetRandomProducts(), Status = this.GetRandomStatus(), UserId = 1 },
            };
        }

        public Task<Order> GetOrder(string id)
        {
            throw new NotImplementedException();
        }

        public Task PlaceOrder(Order order, string userId)
        {
            throw new NotImplementedException();
        }

        private async Task<List<Product>> GetRandomProducts()
        {
            var allProducts = await DependencyService.Get<ProductServiceMock>().GetProductsAsync();
            int bondary= random.Next(0, allProducts.Count);
            return allProducts.GetRange(1, bondary);
        }
        private OrderStatus GetRandomStatus()
        {
            int statusIndex = random.Next(0, 3);
            return (OrderStatus)Enum.GetValues(typeof(OrderStatus)).GetValue(statusIndex);
        }
    }
}
