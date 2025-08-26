using Application.Abstraction;
using Domain.Entities;
using Infrastructure.Persistence.Data;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductoRespository : IProductoRepository
    {
        public bool Create(Producto producto)
        {
            DataTables.Productos.Add(producto);
            return true;
        }

        public List<Producto> GetAll()
        {
            return DataTables.Productos.ToList();
        }
    }
}
