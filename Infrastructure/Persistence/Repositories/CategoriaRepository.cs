using Application.Abstraction;
using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ProductosManagerDbContext _context;

        public CategoriaRepository(ProductosManagerDbContext context)
        {
            _context = context;
        }

        public List<Categoria> GetAll()
        {
            return _context.Categorias.ToList();
        }

        public Categoria? GetById(int id)
        {
            return _context.Categorias.FirstOrDefault(x => x.Id == id);
        }

        public bool Create(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return true;
        }

        public bool Update(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();

            return true;
        }

        public bool Delete(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return true;
        }

        public List<Categoria> GetByCriteria(Expression<Func<Categoria, bool>> expression)
        {
            return _context.Categorias.Where(expression).ToList();
        }

        public bool CheckIfIsAssociatedToAnyProduct(int categoriaId)
        {
            return _context.Libros.Any(p => p.Categoria.Id == categoriaId);
        }
    }
}
