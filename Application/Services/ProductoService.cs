using Application.Data;
using Contracts.Requests;
using Domain.Entities;

namespace Application.Services
{
    public class ProductoService : IProductoService
    {
        public bool Create(ProductoRequest producto)
        {
            bool isSucceded = false;

            if (string.IsNullOrEmpty(producto.Nombre) || producto.Precio <= 0 || producto.Stock < 0)
            {
                return isSucceded;
            }

            producto.Id = DataTables.Productos.Any() ? DataTables.Productos.Max(x => x.Id) + 1 : 1;

            int stock = producto.Stock == null ? 10 : producto.Stock.Value;

            var newProducto = new Producto(producto.Id, producto.Nombre, producto.Precio, stock);

            DataTables.Productos.Add(newProducto);

            isSucceded = true;

            return isSucceded;
        }

        public List<Producto> GetAll()
        {
            var listaProductos = DataTables.Productos.ToList();

            if (listaProductos.Count < 2)
            {
                return new List<Producto>();
            }

            return listaProductos;
        }
    }
}
