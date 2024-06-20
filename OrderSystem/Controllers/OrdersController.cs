using Microsoft.AspNetCore.Mvc;
using OrderSystem.Data;
using OrderSystem.Models;
using System.Net.Http.Json;
using OrderSystem.Services;
using System.Text.Json;

namespace OrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersDbContext _context;
        private readonly CsvService _csvService;

        public OrdersController(OrdersDbContext context, CsvService csvService)
        {
            _context = context;
            _csvService = csvService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            var productImage = await GetProductImageAsync(order.ProductoID);
            if (productImage == null)
            {
                return BadRequest("Producto no encontrado.");
            }

            order.ImagenURL = productImage;
            _context.OrdenesDeCompra.Add(order);
            await _context.SaveChangesAsync();
            _csvService.GenerateCsv();

            return Ok(order);
        }

        private async Task<string?> GetProductImageAsync(int productId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"https://rickandmortyapi.com/api/character/{productId}");
                if (response == null)
                {
                    return null;
                }

                var json = JsonDocument.Parse(response);
                if (json.RootElement.TryGetProperty("image", out var imageProperty))
                {
                    return imageProperty.GetString();
                }

                return null;
            }
        }
    }
}
