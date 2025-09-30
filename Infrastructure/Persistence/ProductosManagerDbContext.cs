using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ProductosManagerDbContext : DbContext
    {
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        public ProductosManagerDbContext(DbContextOptions<ProductosManagerDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Email = "admin@demo.com", Password = "1234", RolId = (int)TipoRol.Administrador },
                new Usuario { Id = 2, Email = "cliente@demo.com", Password = "1234", RolId = (int)TipoRol.Cliente }
            );

            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = nameof(TipoRol.Administrador) },
                new Rol { Id = 2, Nombre = nameof(TipoRol.Cliente) }
            );
        }
    }
}
