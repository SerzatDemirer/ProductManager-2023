using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductManager_2023
{
    public class FashionDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=ProductManager-2023;User Id=sa;Password=Password123;Encrypt=False;");
           
        }
    }
}
