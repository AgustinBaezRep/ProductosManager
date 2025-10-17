using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests
{
    public class CreateProductoRequest
    {
        //public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Range(1, (double)decimal.MaxValue, ErrorMessage = "El rango debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue)]
        public int? Stock { get; set; }
    }
}
