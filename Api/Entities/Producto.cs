using Api.Controllers;

namespace Api.Entities
{
    public class Producto
    {
        public Producto(int id, string nombre, decimal precio, int stock, Categoria? categoria = null)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            Categoria = categoria;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
