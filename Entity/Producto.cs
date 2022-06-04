namespace TestApis.Models
{
    public class Producto
    {
        public int Id { get; init; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public DateTime FechaAlta { get; set; }
        public string SKU { get; set; }
    }
}
