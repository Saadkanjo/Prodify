using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    [Table("Products")]
    public class Product
    {
       public int Id { get; set; }   
       public string Name { get; set; }=string.Empty;
       public string Description { get; set; }=string.Empty;
       public DateTime CreatedOn { get; set; }=DateTime.Now;


        [Column(TypeName ="decimal(18,2)")]
       public int Price { get; set; }
       public List<Feedback> Feedbacks{get;set;}=new List<Feedback>();
       public List<Protfolio> Portfolios { get; set; } = new List<Protfolio>();
    }
}