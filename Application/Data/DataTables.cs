using Domain.Entities;

namespace Application.Data
{
    public static class DataTables
    {
        public static List<Producto> Productos { get; set; } =
        [
            new Producto(1, "Libro A", 1200.00m, 10),
            new Producto(2, "Libro B", 800.00m, 20),
            new Producto(3, "Libro C", 300.00m, 15)
        ];

        public static List<Categoria> Categorias { get; set; } =
        [
            new Categoria { Id = 1, Nombre = "Terror" },
            new Categoria { Id = 2, Nombre = "Cs Ficcion" }
        ];
    }
}
