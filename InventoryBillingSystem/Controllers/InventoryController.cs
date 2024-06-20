using Microsoft.AspNetCore.Mvc;
using InventoryUpdateSystem.Services;

namespace InventoryUpdateSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly CsvProcessorService _csvProcessorService;

        public InventoryController(CsvProcessorService csvProcessorService)
        {
            _csvProcessorService = csvProcessorService;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateInventoryFromCsv()
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, "orders.csv");

            try
            {
                await _csvProcessorService.ProcessCsvAsync(filePath);
                return Ok("Inventory updated from CSV.");
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
