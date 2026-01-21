using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext: IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stock{get;set;}
        public DbSet<Comment> Comment{get;set;}
        public DbSet<Portfolio> Portfolios{get;set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Portfolio>(x=>x.HasKey(p=>new {p.AppUserId,p.StockId}));

            builder.Entity<Portfolio>()
            .HasOne(u=>u.AppUser)
            .WithMany(u=>u.portfolios)
            .HasForeignKey(p => p.AppUserId);

            builder.Entity<Portfolio>()
            .HasOne(u=>u.Stock)
            .WithMany(u=>u.portfolios)
            .HasForeignKey(p => p.StockId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "a8a05a48-3ac2-4299-ad8d-f0c697335785",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "a3c8150f-5704-46d2-b460-110091b0a4bb",
                },
                new IdentityRole
                {
                    Id = "be09ba52-3643-48ae-aa7b-a195d4a85c91",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "3820a473-01bc-4f7a-b3f1-f894aa5e55bd",
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
