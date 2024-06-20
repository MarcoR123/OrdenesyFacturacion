using Microsoft.EntityFrameworkCore;
using BillingSystem.Models;

namespace BillingSystem.Data
{
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options) { }

        public DbSet<OrdenCompra> OrdenesDeCompra { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Factura> Facturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.OrdenCompra)
                .WithMany()
                .HasForeignKey(f => f.OrdenCompraID);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Producto)
                .WithMany()
                .HasForeignKey(f => f.ProductoID);
        }
    }
}
