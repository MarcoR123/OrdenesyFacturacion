namespace BillingSystem.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public int OrdenCompraID { get; set; }
        public int ClienteID { get; set; }
        public int ProductoID { get; set; }
        public string ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }

        public OrdenCompra OrdenCompra { get; set; }
        public Producto Producto { get; set; }
    }
}
