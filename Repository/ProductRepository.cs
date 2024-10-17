using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dto.Product;
using api.Helpers;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductRepository(ApplicationDBContext context)
        {
            _context=context;
        }
        public async Task<Product> CreateAsync(Product productModel)
        {
            await _context.Product.AddAsync(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<Product?> Delete(int id)
        {
             var productModel= await _context.Product.FirstOrDefaultAsync(X=>X.Id==id);
           if (productModel==null){
                return null;
            
            }
            _context.Product.Remove(productModel);
             await _context.SaveChangesAsync();
            return productModel;
        }

        

        public async Task<List<Product>> GetAllSync(QueryObject query)
        {
            var Products=  _context.Product.Include(c=>c.Feedbacks).ThenInclude(a => a.AppUser).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.Name)){
                Products=Products.Where(s => s.Name.Contains(query.Name));
            }
           
            if(!string.IsNullOrWhiteSpace(query.SortBy)){
                if (query.SortBy.Equals("Name",StringComparison.OrdinalIgnoreCase)){
                    Products=query.IsDecsending? Products.OrderByDescending(s=>s.Name): Products.OrderBy(s=>s.Name);
                }
            }
             return await Products.ToListAsync(); 
        }
        

        public async Task<Product?> GetByIdAsync(int id)
        {
           return await _context.Product.Include(c=>c.Feedbacks).ThenInclude(a => a.AppUser).FirstOrDefaultAsync(i=>i.Id==id);
        }

        public  async Task<Product?> GetByNameAsync(string name)
        {
             return await _context.Product.FirstOrDefaultAsync(s => s.Name == name);
        }

        public  Task<bool> ProductExists(int id)
        {
            return _context.Product.AnyAsync(s=>s.Id==id);
        }
        

        public async Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productRequestDto)
        {
           var existingProduct= await _context.Product.FirstOrDefaultAsync(x=>x.Id==id);
             if (existingProduct==null){
                return null;
             }
            existingProduct.Name=productRequestDto.Name;
            existingProduct.Description=productRequestDto.Description;
            existingProduct.Price= productRequestDto.price;
            
             await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}