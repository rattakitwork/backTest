using Microsoft.EntityFrameworkCore;

namespace TestAPI.Controllers
{
    public class FoodDbContext : DbContext
    {
        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options)
        {
        }
        public DbSet<FoodModels> Foods { get; set; }
    }

}
