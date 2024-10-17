using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    [Table("Feedbacks")]
    public class Feedback
    
    {
        public int Id { get; set; }
        public string Comment {get;set;}=string.Empty;
        public DateTime CreatedOn { get; set; }=DateTime.Now;
        public  int?  ProductId { get; set; }
        //navigation
        public Product? product { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}