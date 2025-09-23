using Application.Abstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductoRespository : BaseRepository<Libro>, IProductoRepository
    {
        private readonly ProductosManagerDbContext _context;

        public ProductoRespository(ProductosManagerDbContext context) : base(context)
        {
            _context = context;
        }

        public override List<Libro> GetAll()
        {
            return _context.Libros.Include(p => p.Categoria).ToList();
        }   
    }
}
