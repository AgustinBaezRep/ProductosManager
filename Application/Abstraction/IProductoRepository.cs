using Contracts.Requests;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Abstraction
{
    public interface IProductoRepository
    {
        List<Libro> GetAll();
        Libro? GetById(int id);
        bool Create(Libro producto);
        bool Update(Libro producto);
        bool UpdateKeyMetadata(Libro producto);
        bool Delete(Libro producto);
        List<Libro> GetByCriteria(Expression<Func<Libro, bool>> expression);
    }
}
