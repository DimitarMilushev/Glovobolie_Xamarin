using GlovobolieApp.Helpers;
using GlovobolieApp.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace GlovobolieApp.Services.OrderService
{
    public class OrderService : DataServiceBase, IOrderService
    {
        public Task<ICollection<Order>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task PlaceOrderAsync(Order order, int userId)
        {
            string query = "INSERT INTO `orders`( `date_ordered`, `status_id`, `user_id`)" +
                $" VALUES ('{DateHelper.FormatMySQL(order.Date)}','{(int)order.Status}','{userId}')";
            int orderId = await Task.Run(() => this.PlaceOrder(query));
            await Task.Run(() => this.CreateOrderedProducts(order.Products, orderId));
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
                return (int)orderId;
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
