using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Product;
using api.Helpers;
using api.models;

namespace api.Interfaces
{
    public interface IProductRepository
    {
        Task <List<Product>> GetAllSync(QueryObject query);
        Task <Product?> GetByIdAsync(int id);
        Task<Product?> GetByNameAsync(string name);
        Task<Product> CreateAsync(Product productModel);
        Task<Product?> UpdateAsync(int id,UpdateProductRequestDto productRequestDto);
        Task<Product?> Delete( int id);
        Task<bool> ProductExists(int id);
        
    }
}