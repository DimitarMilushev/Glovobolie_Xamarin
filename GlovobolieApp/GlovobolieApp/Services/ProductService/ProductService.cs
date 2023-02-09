using GlovobolieApp.Artifacts.RepositoryService;
using GlovobolieApp.Models;
using GlovobolieApp.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.Artifacts.ProductService
{
    public class ProductService : DataServiceBase, IProductService
    {
        public Task<Product> GetProductByIdAsync(int productId)
        {
            string query = $"SELECT * FROM products HAVING id = '{productId}' LIMIT = 1;";
            return Task.Run<Product>(() => this.GetProductById(query));
        }

        public Task<List<Product>> GetAllProductsAsync() => Task.Run<List<Product>>(this.GetAllProducts);

        public Task<List<Product>> GetProductsByOrderAsync(int orderId)
        {
            string query = "SELECT p.id, p.title, p.description, p.price, p.image_url, o.quantity " +
                           "FROM `products` AS p " +
                           "INNER JOIN ordered_products AS o " +
                           "ON o.product_id = p.id " +
                           $"WHERE o.order_id = {orderId}";


            return Task.Run<List<Product>>(() => this.GetProductsByOrder(query));
        }

        private List<Product> GetProductsByOrder(string query)
        {
            var products = new List<Product>();
            try
            {
                connection.Open();
                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = reader.GetInt32("id"),
                        Title = reader.GetString("title"),
                        Description = reader.GetString("description"),
                        Price = reader.GetDouble("price"),
                        ImageURL = reader.GetString("image_url"),
                        Quantity = reader.GetInt32("quantity")
                    });
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return products;

        }


        // Synchronized methods - MySQL has bad support for Async calls.

        private Product GetProductById(string query)
        {
            try
            {
                connection.Open();
                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();
                if (!reader.Read()) throw new Exception("Failed to find product by id!");
                return new Product
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Description = reader.GetString("description"),
                    Price = reader.GetDouble("price"),
                    ImageURL = reader.GetString("image_url"),
                    Quantity = 1
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        private List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM products;", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = reader.GetInt32("id"),
                        Title = reader.GetString("title"),
                        Description = reader.GetString("description"),
                        Price = reader.GetDouble("price"),
                        ImageURL = reader.GetString("image_url"),
                        Quantity = 1
                    }); ;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return products;
        }
    }
}
