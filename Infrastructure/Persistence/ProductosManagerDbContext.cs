using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ProductosManagerDbContext : DbContext
    {
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public ProductosManagerDbContext(DbContextOptions<ProductosManagerDbContext> options) : base(options)
        {
            
        }
    }
}
