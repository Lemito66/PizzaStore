using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public class PizzaDbContext : DbContext
    {

        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options)
        {

        }

        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
    }
}
