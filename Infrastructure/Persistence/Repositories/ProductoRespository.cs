using Application.Abstraction;
using Contracts.Requests;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductoRespository : IProductoRepository
    {
        public ProductoRespository() { }

        public List<Producto> GetAll()
        {
            return DataTables.Productos.ToList();
        }

        public Producto? GetById(int id)
        {
            return DataTables.Productos.FirstOrDefault(x => x.Id == id);
        }

        public bool Create(Producto producto)
        {
            DataTables.Productos.Add(producto);

            return true;
        }

        public bool Update(Producto producto, UpdateProductoRequest request)
        {
            producto.Nombre = request.Nombre;
            producto.Precio = request.Precio;
            producto.Stock = request.Stock;

            return true;
        }

        public bool UpdateKeyMetadata(Producto producto, UpdateKeyMetadataProductoRequest request)
        {
            producto.Nombre = request.Nombre ?? producto.Nombre;
            producto.Precio = request.Precio ?? producto.Precio;
            producto.Stock = request.Stock ?? producto.Stock;

            return true;
        }

        public bool Delete(Producto producto)
        {
            return DataTables.Productos.Remove(producto);
        }

        public List<Producto> GetByCriteria(Expression<Func<Producto, bool>> expression)
        {
            return DataTables.Productos.AsQueryable().Where(expression).ToList();
        }
    }
}
