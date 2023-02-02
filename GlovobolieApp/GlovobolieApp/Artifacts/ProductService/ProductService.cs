//using GlovobolieApp.Artifacts.RepositoryService;
//using GlovobolieApp.Models;
//using MySqlConnector;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Threading.Tasks;
//using Xamarin.Forms;

//namespace GlovobolieApp.Artifacts.ProductService
//{
//    public class ProductService
//    {
//        private MySqlConnection repository;
//        public ProductService()
//        {
//            repository = DependencyService.Get<Repository>().connection;
//        }

//        public async Task<List<Product>> GetProductsAsync()
//        {
//            List<Product> products = new List<Product>();
//            try
//            {
//                await repository.OpenAsync();
//                var command = new MySqlCommand("SELECT field FROM product;", repository);
//                var asyncReader = await command.ExecuteReaderAsync();
//                while (await asyncReader.ReadAsync())
//                {
//                    products.Add(new Product
//                    {
//                        Id = asyncReader.GetInt32("id"),
//                        Title = asyncReader.GetString("title"),
//                        Description = asyncReader.GetString("description"),
//                        Price = asyncReader.GetDouble("price"),
//                        ImageURL = asyncReader.GetString("imageURL"),
//                    });
//                }
//            }
//            catch (Exception e)
//            {
//                Debug.WriteLine(e.Message);
//            }
//            finally
//            {
//                await repository.CloseAsync();
//            }
//            return products;
//        }
//    }
//}
