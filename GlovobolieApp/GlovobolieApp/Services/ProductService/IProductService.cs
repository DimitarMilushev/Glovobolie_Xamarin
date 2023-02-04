using GlovobolieApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlovobolieApp.Artifacts.ProductService
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
    }
}