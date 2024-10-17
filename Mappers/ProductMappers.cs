using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Product;
using api.models;

namespace api.Mappers
{
    public static class ProductMappers
    {
         public static ProductDto ToProductDto(this Product productModel)
      {
            return new ProductDto
            {
                Id = productModel.Id,
                Name=productModel.Name,
                Description=productModel.Description,
                CreatedOn= productModel.CreatedOn,
                price = productModel.Price,
                feedbacks=productModel.Feedbacks.Select(c=>c.ToFeedbackDto()).ToList() 
            };
      }
        public static Product ToProductFromCreateDto(this CreateProductRequestDto ProductDto){
         return new Product{
                Name=ProductDto.Name,
                Description=ProductDto.Description,
                Price = ProductDto.price,

         };
      
    }
    }
}