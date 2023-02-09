using GlovobolieApp.Artifacts.ProductService;
using GlovobolieApp.Helpers;
using GlovobolieApp.Models;
using GlovobolieApp.Models.Enums;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.Services.OrderService
{
    public class OrderService : DataServiceBase, IOrderService
    {
        public async Task<ICollection<Order>> GetAllOrdersByUserAsync(int userId)
        {
            string query = $"SELECT * FROM `orders` HAVING user_id = '{userId}';";
            ICollection<Order> orders = await Task.Run(() => this.GetAllOrdersByUser(query, userId));
            foreach(var order in orders)
            {
                order.Products = await DependencyService.Get<IProductService>().GetProductsByOrderAsync(order.Id);
            }
            return orders;
        }

        public async Task PlaceOrderAsync(Order order)
        {
            string query = "INSERT INTO `orders`( `date_ordered`, `status_id`, `user_id`)" +
                $" VALUES ('{DateHelper.FormatMySQL(order.Date)}','{(int)order.Status}','{order.UserId}')";
            int orderId = await Task.Run(() => this.PlaceOrder(query));
            await Task.Run(() => this.CreateOrderedProducts(order.Products, orderId));
        }
        private ICollection<Order> GetAllOrdersByUser(string query, int userId)
        {
            List<Order> orders = new List<Order>();
            try
            {
                this.connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    orders.Add(new Order
                    {
                        Id= reader.GetInt32("id"),
                        Date = reader.GetDateTime("date_ordered"),
                        Status = (OrderStatus)reader.GetInt32("status_id"),
                        UserId = userId
                    });
                }
                return orders;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
            finally { this.connection.Close(); }
        }
        private void CreateOrderedProducts(ICollection<Product> products, int orderId)
        {
            try
            {
                this.connection.Open();
                string query;
                MySqlCommand command;
                foreach (var product in products)
                {
                    query = "INSERT INTO `ordered_products`(`order_id`, `product_id`, `quantity`)" +
                        $" VALUES ('{orderId}','{product.Id}','{product.Quantity}')";
                    command = new MySqlCommand(query, connection);
                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 1) throw new Exception("Failed to insert into ordered_products!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
            finally { this.connection.Close(); }
        }
        private int PlaceOrder(string query)
        {
            try
            {
                this.connection.Open();
                var command = new MySqlCommand(query, connection);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected != 1) throw new Exception("Failed to insert into orders!");
                var orderIdCommand = new MySqlCommand("SELECT LAST_INSERT_ID();", connection);
                var orderId = orderIdCommand.ExecuteScalar();
                if (orderId == null) throw new Exception("Failed to find new order!");
                return int.Parse(orderId.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
            finally { this.connection.Close(); }
        }
    }
}
