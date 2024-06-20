using Microsoft.EntityFrameworkCore;
using InventoryUpdateSystem.Models;

namespace InventoryUpdateSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
    }
}
