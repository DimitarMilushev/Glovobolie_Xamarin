using GlovobolieApp.Models;
using GlovobolieApp.Services.RepositoryService;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlovobolieApp.Services.ProductService
{
    public class ProductService : DataService<Product>
    {
        private static readonly string collectionName = "product";
        public ProductService(Repository repository) 
            : base(repository.GetRepositoryCollection<Product>(collectionName))
        {}
    }
}
