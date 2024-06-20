using OrderSystem.Data;
using System.Text;
using System.IO;

namespace OrderSystem.Services
{
    public class CsvService
    {
        private readonly OrdersDbContext _context;

        public CsvService(OrdersDbContext context)
        {
            _context = context;
        }

        public void GenerateCsv()
        {
            var orders = _context.OrdenesDeCompra.ToList();
            var csv = new StringBuilder();
            csv.AppendLine("ID,ProductoID,ImagenURL,Cantidad,ClienteID,Fecha");

            foreach (var order in orders)
            {
                csv.AppendLine($"{order.ID},{order.ProductoID},{order.ImagenURL},{order.Cantidad},{order.ClienteID},{order.Fecha.ToString("dd/MM/yyyy HH:mm:ss")}");
            }

            // Obtener el escritorio del usuario
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, "orders.csv");

            File.WriteAllText(filePath, csv.ToString());
        }
    }
}
