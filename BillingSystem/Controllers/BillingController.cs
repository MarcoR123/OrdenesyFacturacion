using Microsoft.AspNetCore.Mvc;
using BillingSystem.Data;
using BillingSystem.Models;
using BillingSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace BillingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly BillingDbContext _context;
        private readonly InvoiceService _invoiceService;

        public BillingController(BillingDbContext context, InvoiceService invoiceService)
        {
            _context = context;
            _invoiceService = invoiceService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateInvoices()
        {
            await _invoiceService.GenerateInvoicesAsync();
            return Ok(new { message = "Invoices generated successfully." });
        }

        [HttpGet("facturas")]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await _context.Facturas.ToListAsync();
            return Ok(invoices);
        }

        [HttpPost("generate/{orderId}")]
        public async Task<IActionResult> GenerateInvoiceByOrderId(int orderId)
        {
            var order = await _context.OrdenesDeCompra.FindAsync(orderId);
            if (order == null)
            {
                return NotFound($"Order with ID {orderId} not found.");
            }

            var producto = await _context.Productos.FindAsync(order.ProductoID);
            if (producto == null)
            {
                return NotFound($"Product with ID {order.ProductoID} not found.");
            }

            var invoice = new Factura
            {
                OrdenCompraID = order.ID,
                ClienteID = order.ClienteID,
                ProductoID = order.ProductoID,
                ProductoNombre = producto.Nombre,
                Cantidad = order.Cantidad,
                Precio = order.Cantidad * producto.Precio,
                Fecha = DateTime.Now
            };

            _context.Facturas.Add(invoice);
            await _context.SaveChangesAsync();

            return Ok(invoice);
        }

    }
}
