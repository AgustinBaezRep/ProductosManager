using Application.Abstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductoRespository : IProductoRepository
    {
        private readonly ProductosManagerDbContext _context;

        public ProductoRespository(ProductosManagerDbContext context)
        {
            _context = context;
        }

        public List<Libro> GetAll()
        {
            return _context.Libros.Include(c => c.Categoria).ToList();
        }

        public Libro? GetById(int id)
        {
            return _context.Libros.Include(c => c.Categoria).FirstOrDefault(x => x.Id == id);
        }

        public bool Create(Libro producto)
        {
            _context.Libros.Add(producto);
            _context.SaveChanges();

            return true;
        }

        public bool Update(Libro producto)
        {
            _context.Libros.Update(producto);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateKeyMetadata(Libro producto)
        {
            _context.Libros.Update(producto);
            _context.SaveChanges();

            return true;
        }

        public bool Delete(Libro producto)
        {
            _context.Libros.Remove(producto);
            _context.SaveChanges();

            return true;
        }

        public List<Libro> GetByCriteria(Expression<Func<Libro, bool>> expression)
        {
            return _context.Libros.Include(c => c.Categoria).Where(expression).ToList();
        }
    }
}
