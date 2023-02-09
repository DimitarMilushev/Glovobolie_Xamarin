using GlovobolieApp.Artifacts.ProductService;
using GlovobolieApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GlovobolieApp.Services.ProductService
{
    public class ProductServiceMock : IProductService
    {
        public Task<List<Product>> GetProductsAsync() => Task.Run(() =>
        {
            System.Threading.Thread.Sleep(1000);
            return new List<Product>()
            {
                new Product { Id =1, Title = "First item", Description="This is an item description.", Price = 13.2 , ImageURL = "https://thumbs.dreamstime.com/z/chef-hotel-restaurant-kitchen-cooking-hands-prepared-beef-steak-vegetable-decoration-81415061.jpg", Quantity = 1},
                new Product { Id = 2, Title = "Third item", Description="This is an item description." , Price = 5.3 , ImageURL = "https://thumbs.dreamstime.com/z/italian-food-white-plate-36425880.jpg", Quantity = 1},
                new Product { Id = 3, Title = "Fourth item", Description="This is an item description.", Price = 6.5 , ImageURL = "https://thumbs.dreamstime.com/b/arabic-food-kabsa-chicken-rice-vegetables-close-up-ho-plate-horizontal-view-above-69259015.jpg", Quantity = 1},
                new Product { Id = 4, Title = "Fifth item", Description="This is an item description." , Price = 3.5 , ImageURL = "https://thumbs.dreamstime.com/b/fast-food-menu-chicken-nuggets-hamburger-french-fries-33671451.jpg", Quantity = 1},
                new Product { Id = 5, Title = "Sixth item", Description="This is an item description." , Price = 10.5, ImageURL = "https://thumbs.dreamstime.com/b/bombay-potato-curry-indian-food-29146242.jpg", Quantity = 1}
            };
        });
    }
}
