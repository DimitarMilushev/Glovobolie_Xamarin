using GlovobolieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlovobolieApp.Services
{
    public class MockDataStore : IDataStore<Product>
    {
        readonly List<Product> items;

        public MockDataStore()
        {
            items = new List<Product>()
            {
                new Product { Id = Guid.NewGuid().ToString(), Title = "First item", Description="This is an item description.", Price = 13.2 , ImageURL = "https://thumbs.dreamstime.com/z/chef-hotel-restaurant-kitchen-cooking-hands-prepared-beef-steak-vegetable-decoration-81415061.jpg"},
                new Product { Id = Guid.NewGuid().ToString(), Title = "Second item", Description="This is an item description.", Price = 3.5 , ImageURL = "https://thumbs.dreamstime.com/z/bowl-chili-13727177.jpg"        },
                new Product { Id = Guid.NewGuid().ToString(), Title = "Third item", Description="This is an item description." , Price = 5.3 , ImageURL = "https://thumbs.dreamstime.com/z/italian-food-white-plate-36425880.jpg"  },
                new Product { Id = Guid.NewGuid().ToString(), Title = "Fourth item", Description="This is an item description.", Price = 6.5 , ImageURL = "https://thumbs.dreamstime.com/b/arabic-food-kabsa-chicken-rice-vegetables-close-up-ho-plate-horizontal-view-above-69259015.jpg"   },
                new Product { Id = Guid.NewGuid().ToString(), Title = "Fifth item", Description="This is an item description." , Price = 3.5 , ImageURL = "https://thumbs.dreamstime.com/b/fast-food-menu-chicken-nuggets-hamburger-french-fries-33671451.jpg"    },
                new Product { Id = Guid.NewGuid().ToString(), Title = "Sixth item", Description="This is an item description." , Price = 10.5, ImageURL = "https://thumbs.dreamstime.com/b/bombay-potato-curry-indian-food-29146242.jpg"        }
            };
        }

        public async Task<bool> AddItemAsync(Product item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Product item)
        {
            var oldItem = items.Where((Product arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Product arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Product> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}