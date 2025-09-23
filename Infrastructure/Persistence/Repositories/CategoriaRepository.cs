using Application.Abstraction;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        private readonly ProductosManagerDbContext _context;

        public CategoriaRepository(ProductosManagerDbContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckIfIsAssociatedToAnyProduct(int categoriaId)
        {
            return _context.Libros.Any(p => p.Categoria.Id == categoriaId);
        }
    }
}
