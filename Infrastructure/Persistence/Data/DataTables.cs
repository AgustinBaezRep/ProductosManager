using Domain.Entities;

namespace Infrastructure.Persistence.Data
{
    public static class DataTables
    {
        public static List<Producto> Productos { get; set; } =
        [
            new Producto() { Id = 1, Nombre = "Libro A", Precio = 1200.00m, Stock = 10 },
            new Producto() { Id = 2, Nombre = "Libro B", Precio = 800.00m, Stock = 20 },
            new Producto() { Id = 3, Nombre = "Libro C", Precio = 300.00m, Stock = 15 }
        ];

        public static List<Categoria> Categorias { get; set; } =
        [
            new Categoria { Id = 1, Nombre = "Terror" },
            new Categoria { Id = 2, Nombre = "Cs Ficcion" }
        ];
    }
}
