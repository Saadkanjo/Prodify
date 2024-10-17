using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Feedback
{
    public class FeedbackDto
    {
         public int Id { get; set; }
        public string Comment {get;set;}=string.Empty;
        public DateTime CreatedOn { get; set; }=DateTime.Now;
        public  int?  ProductId { get; set; }
        public string CreatedBy {get; set;} = string.Empty;

        //navigation
        
    }
}