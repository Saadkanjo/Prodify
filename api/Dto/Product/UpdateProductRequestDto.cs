using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Product
{
    public class UpdateProductRequestDto
    {
       public string Name { get; set; }=string.Empty;
       public string Description { get; set; }=string.Empty;
       public int price { get; set; }
    }
}