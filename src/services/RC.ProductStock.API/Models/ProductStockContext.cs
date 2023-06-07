using System;
using Microsoft.EntityFrameworkCore;

namespace ProductStock.Models 
{
    public  class ProductStockContext : DbContext
    {
        public DbSet<Product> Products { get; set; }  

        public ProductStockContext(DbContextOptions<ProductStockContext> options) : base(options)
        {

        }
    }
}
