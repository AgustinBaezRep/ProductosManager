using Application.Contracts;
using Application.Entities;

namespace Application.Services
{
    public interface IProductoService
    {
        List<Producto> GetAll();
        bool Create(ProductoRequest request);
    }
}
