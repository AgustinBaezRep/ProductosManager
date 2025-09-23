namespace Domain.Entities
{
    public class Libro : BaseEntity
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
