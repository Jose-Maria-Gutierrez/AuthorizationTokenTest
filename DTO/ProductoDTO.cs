using System.ComponentModel.DataAnnotations;

namespace TestApis.DTO
{
    public class ProductoDTO
    {
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; }
        [Required]
        [Range(0,9999)]
        public double Precio { get; set; }
        [Required]
        [MaxLength(6)]
        [SKUValidation]
        public string SKU { get; set; }
    }
}
