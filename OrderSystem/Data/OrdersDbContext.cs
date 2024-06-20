using Microsoft.EntityFrameworkCore;
using OrderSystem.Models;

namespace OrderSystem.Data
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }

        public DbSet<Order> OrdenesDeCompra { get; set; }
    }
}
