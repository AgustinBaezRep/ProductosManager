using Contracts.Requests;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Abstraction
{
    public interface IProductoRepository
    {
        List<Producto> GetAll();
        Producto? GetById(int id);
        bool Create(Producto producto);
        bool Update(Producto producto, ProductoRequest request);
        bool Delete(Producto producto);
        List<Producto> GetByCriteria(Expression<Func<Producto, bool>> expression);
    }
}
