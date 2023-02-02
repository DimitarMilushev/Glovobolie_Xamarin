using GlovobolieApp.Artifacts.RepositoryService;
using GlovobolieApp.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.Artifacts.ProductService
{
    public class ProductService
    {
        private MySqlConnection connection;
        public ProductService()
        {
            connection = DependencyService.Get<Repository>().connection;
        }

        public Task<List<Product>> GetProductsAsync() => Task.Run<List<Product>>(this.GetProducts);

        private List<Product> GetProducts()
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
                        ImageURL = reader.GetString("imageURL"),
                        Rating = reader.GetDouble("rating"),
                    });
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
