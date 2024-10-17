using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
      {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _ProductRepo;
        private readonly IProtfolioRepository _portfolioRepo;
        
    //CTOR
         public PortfolioController(UserManager<AppUser> userManager,
        IProductRepository productRepo, IProtfolioRepository portfolioRepo)
         {
            _userManager = userManager;
            _ProductRepo = productRepo;
            _portfolioRepo = portfolioRepo;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();//the User is being ihereted from the ControllerBase(gonna allow us to reach in and grab everything associated with the user and the claims)
            var appUser = await _userManager.FindByNameAsync(username);

//find the user's porfolio
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio (string name)
        {
        //1st step get the user
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
        //2nd step get the product
            var product = await _ProductRepo.GetByNameAsync(name);

            if (product == null) return BadRequest("Product not found");

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

//check if we already have the stock added
            if (userPortfolio.Any(e => e.Name.ToLower() == name.ToLower())) return BadRequest("Cannot add same stock to portfolio");

//3rd step create the portfolio object 
            var portfolioModel = new Protfolio
            {
                ProductId = product.Id,
                AppUserId = appUser.Id
            };

            await _portfolioRepo.CreateAsync(portfolioModel);

            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }
       [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string name)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            
        //get all the the stocks in the userPortfolio
          var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

		//check if the stock is there
            var filteredProduct = userPortfolio.Where(s => s.Name.ToLower() == name.ToLower()).ToList();


if (filteredProduct.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolio(appUser, name);
            }
            else
            {
                return BadRequest("product not in your portfolio");
            }

            return Ok();
        }



}}