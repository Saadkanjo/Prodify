using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
                
              public ApplicationDBContext( DbContextOptions<ApplicationDBContext> options) : base(options)
              {
                
                                
              }
                public DbSet<Product> Product { get; set; }
                public DbSet<Feedback> Feedback { get; set; }
                public DbSet<Protfolio> Portfolios { get; set; }
  protected override void OnModelCreating(ModelBuilder builder)
{
 base.OnModelCreating(builder);
 //setting up the forieng keys
 builder.Entity<Protfolio>(x => x.HasKey(p => new { p.AppUserId, p.ProductId }));

//connect the FKs into the tables
				 builder.Entity<Protfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);
            
		        builder.Entity<Protfolio>()
                .HasOne(u => u.Product)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.ProductId);

 List<IdentityRole> roles = new List<IdentityRole>{
 new IdentityRole {Name = "Admin", NormalizedName ="ADMIN"},
                //NormalizedName is just capatilized
 new IdentityRole {Name = "User", NormalizedName = "USER"},
 };
 
 builder.Entity<IdentityRole>().HasData(roles);
   }

             
    }
}