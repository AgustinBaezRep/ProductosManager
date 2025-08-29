using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Abstraction
{
    public interface ICategoriaRepository
    {
        List<Categoria> GetAll();
        Categoria? GetById(int id);
        bool Create(Categoria producto);
        bool Update(Categoria producto);
        bool Delete(Categoria producto);
        List<Categoria> GetByCriteria(Expression<Func<Categoria, bool>> expression);
    }
}
