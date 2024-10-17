using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Feedback;
using api.models;

namespace api.Dto.Product
{
    public class ProductDto
    {
        public int Id { get; set; }   
       public string Name { get; set; }=string.Empty;
       public string Description { get; set; }=string.Empty;
       public DateTime CreatedOn { get; set; }=DateTime.Now;
       public int price { get; set; }
       public List<FeedbackDto> feedbacks{get;set;}=new List<FeedbackDto>();
    }
}