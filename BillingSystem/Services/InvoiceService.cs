using System.Threading.Tasks;
using BillingSystem.Data;
using BillingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingSystem.Services
{
    public class InvoiceService
    {
        private readonly BillingDbContext _context;

        public InvoiceService(BillingDbContext context)
        {
            _context = context;
        }

        public async Task GenerateInvoicesAsync()
        {
            var ordenes = await _context.OrdenesDeCompra.ToListAsync();

            foreach (var orden in ordenes)
            {
                var producto = await _context.Productos.FindAsync(orden.ProductoID);
                if (producto != null)
                {
                    var totalPrecio = producto.Precio * orden.Cantidad;

                    var factura = new Factura
                    {
                        OrdenCompraID = orden.ID,
                        ClienteID = orden.ClienteID,
                        ProductoID = producto.Id,
                        ProductoNombre = producto.Nombre,
                        Cantidad = orden.Cantidad,
                        Precio = totalPrecio, // Calcular el precio total
                        Fecha = orden.Fecha
                    };

                    _context.Facturas.Add(factura);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
