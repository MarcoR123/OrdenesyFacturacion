using System.Globalization;
using System.IO;
using System.Text;
using InventoryUpdateSystem.Data;
using InventoryUpdateSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryUpdateSystem.Services
{
    public class CsvProcessorService
    {
        private readonly ApplicationDbContext _context;

        public CsvProcessorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ProcessCsvAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' was not found.");
            }

            using (var reader = new StreamReader(filePath))
            {
                bool firstLine = true;
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (firstLine)
                    {
                        firstLine = false;
                        continue; // Skip header line
                    }

                    var values = line.Split(',');

                    var productoId = int.Parse(values[1]);
                    var cantidad = int.Parse(values[3]);

                    var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == productoId);
                    if (producto != null)
                    {
                        producto.Stock -= cantidad; // Reduce the stock based on the order quantity
                        _context.Productos.Update(producto);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
