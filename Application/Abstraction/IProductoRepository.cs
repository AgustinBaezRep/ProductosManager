using Domain.Entities;

namespace Application.Abstraction
{
    public interface IProductoRepository
    {
        List<Producto> GetAll();
        bool Create(Producto producto);
    }
}
