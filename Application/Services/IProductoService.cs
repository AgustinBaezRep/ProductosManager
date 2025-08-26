using Contracts.Requests;
using Domain.Entities;

namespace Application.Services
{
    public interface IProductoService
    {
        List<Producto> GetAll();
        bool Create(ProductoRequest request);
    }
}
