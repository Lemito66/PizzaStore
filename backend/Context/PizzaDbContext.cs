using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public class PizzaDbContext : DbContext
    {

        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
