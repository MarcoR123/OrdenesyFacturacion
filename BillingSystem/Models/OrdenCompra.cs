namespace BillingSystem.Models
{
    public class OrdenCompra
    {
        public int ID { get; set; }
        public int ProductoID { get; set; }
        public string ImagenURL { get; set; }
        public int Cantidad { get; set; }
        public int ClienteID { get; set; }
        public DateTime Fecha { get; set; }
    }
}
