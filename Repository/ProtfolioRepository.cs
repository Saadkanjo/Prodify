using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProtfolioRepository : IProtfolioRepository
    {
        private readonly ApplicationDBContext _context;
        public ProtfolioRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<Protfolio> CreateAsync(Protfolio protfolio)
        {
            await _context.Portfolios.AddAsync(protfolio);
            await _context.SaveChangesAsync();
            return protfolio;
        }

        public async Task<Protfolio> DeletePortfolio(AppUser appUser, string name)
        {
         var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Product.Name.ToLower() == name.ToLower());

            if (portfolioModel == null)
            {
                return null;
            }

            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Product>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
            .Select(Product => new Product
            {
                Id = Product.ProductId,
                Name = Product.Product.Name,
                Description = Product.Product.Description,
                Price = Product.Product.Price,
                CreatedOn = Product.Product.CreatedOn
                
            }).ToListAsync();
        }
        }
    }
