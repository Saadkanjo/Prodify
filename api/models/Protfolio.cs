using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    [Table("Protfolios")]
    public class Protfolio
    {
        public string AppUserId { get; set; } // Foreign key to AppUser
        public int ProductId { get; set; } // Foreign key to Product

        // Navigation properties
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }
    }
}
