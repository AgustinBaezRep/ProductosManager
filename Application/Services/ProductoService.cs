using Application.Abstraction;
using Contracts.Requests;
using Contracts.Responses;
using Domain.Entities;

namespace Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public List<ProductoResponse> GetAll()
        {
            var listaProductos = _productoRepository.GetAll()
                .Select(p => new ProductoResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock
                })
                .ToList();

            if (listaProductos.Count < 2)
            {
                return new List<ProductoResponse>();
            }

            return listaProductos;
        }

        public bool Create(ProductoRequest producto)
        {
            var productos = _productoRepository.GetAll();

            if (string.IsNullOrEmpty(producto.Nombre) || producto.Precio <= 0 || producto.Stock < 0)
            {
                return false;
            }

            producto.Id = productos.Any() ? productos.Max(x => x.Id) + 1 : 1;

            int stock = producto.Stock == null ? 10 : producto.Stock.Value;

            var newProducto = new Producto(producto.Id, producto.Nombre, producto.Precio, stock);

            return _productoRepository.Create(newProducto);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool DisassociateCategory(int id)
        {
            throw new NotImplementedException();
        }

        public bool AssociateCategory(int id, int categoriaId)
        {
            throw new NotImplementedException();
        }

        public ProductoResponse? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductoResponse> GetByValue(decimal valor)
        {
            throw new NotImplementedException();
        }

        public int GetTotalProductos()
        {
            throw new NotImplementedException();
        }

        public List<ProductoResponse> Search(string? name, int? categoriaId, decimal? pMin, decimal? pMax)
        {
            throw new NotImplementedException();
        }

        public bool Update(ProductoRequest request)
        {
            throw new NotImplementedException();
        }

        public bool UpdateKeyMetadata(int id, UpdateKeyMetadataRequest producto)
        {
            throw new NotImplementedException();
        }
    }
}
