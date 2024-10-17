using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;

namespace api.Interfaces
{
    public interface IProtfolioRepository
    {
      Task<List<Product>> GetUserPortfolio(AppUser user);
      Task<Protfolio> CreateAsync(Protfolio protfolio);
      Task<Protfolio> DeletePortfolio(AppUser appUser, string name);
    }
}