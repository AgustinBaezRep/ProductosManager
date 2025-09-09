using Application.Abstraction;
using Contracts.Requests;
using Contracts.Responses;
using Domain.Entities;

namespace Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProductoService(IProductoRepository productoRepository, ICategoriaRepository categoriaRepository)
        {
            _productoRepository = productoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public List<ProductoResponse> GetAll()
        {
            var listaProductos = _productoRepository
                .GetAll()
                .Select(p => new ProductoResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    Categoria = p.Categoria
                }).ToList();

            return listaProductos;
        }

        public bool Create(CreateProductoRequest producto)
        {
            var productos = _productoRepository.GetAll();

            producto.Id = productos.Any() ? productos.Max(x => x.Id) + 1 : 1;

            producto.Stock = producto.Stock == null ? 10 : producto.Stock.Value;

            var newProducto = new Libro()
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock.Value
            };

            return _productoRepository.Create(newProducto);
        }

        public bool Delete(int id)
        {
            var producto = _productoRepository.GetById(id);

            if (producto == null)
            {
                return false;
            }

            return _productoRepository.Delete(producto);
        }

        public bool DisassociateCategory(int id)
        {
            var producto = _productoRepository.GetById(id);

            if (producto == null)
            {
                return false;
            }

            producto.Categoria = null;

            return _productoRepository.Update(producto);
        }

        public bool AssociateCategory(int id, int categoriaId)
        {
            var producto = _productoRepository.GetById(id);

            var categoria = _categoriaRepository.GetById(categoriaId);

            if (producto == null || categoria == null)
            {
                return false;
            }

            if (producto.Categoria != null)
            {
                return false;
            }

            producto.Categoria = categoria;

            return _productoRepository.Update(producto);
        }

        public ProductoResponse? GetById(int id)
        {
            return _productoRepository.GetById(id) is Libro producto
                ? new ProductoResponse()
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    Categoria = producto.Categoria
                }
                : null;
        }

        public List<ProductoResponse> GetByValue(decimal valor)
        {
            var listaProductos = _productoRepository
                .GetByCriteria(x => x.Precio >= valor)
                .Select(s => new ProductoResponse()
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Precio = s.Precio,
                    Stock = s.Stock,
                    Categoria = s.Categoria
                }).ToList();

            return listaProductos;
        }

        public int GetTotalProductos()
        {
            return _productoRepository.GetAll().Count;
        }

        public List<ProductoResponse> Search(string? name, int? categoriaId, decimal? pMin, decimal? pMax)
        {
            var listaProductos = _productoRepository
                .GetByCriteria(x =>
                    (string.IsNullOrWhiteSpace(name) || x.Nombre.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                    (categoriaId == null || (x.Categoria != null && x.Categoria.Id == categoriaId.Value)) &&
                    (pMin == null || x.Precio >= pMin.Value) &&
                    (pMax == null || x.Precio <= pMax.Value))
                .Select(s => new ProductoResponse()
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Precio = s.Precio,
                    Stock = s.Stock,
                    Categoria = s.Categoria
                }).ToList();

            return listaProductos;
        }

        public bool Update(int id, UpdateProductoRequest request)
        {
            var productoExistente = _productoRepository.GetById(id);

            if (productoExistente == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(request.Nombre) || request.Precio <= 0)
            {
                return false;
            }

            productoExistente.Nombre = request.Nombre;
            productoExistente.Precio = request.Precio;
            productoExistente.Stock = request.Stock;

            return _productoRepository.Update(productoExistente);
        }

        public bool UpdateKeyMetadata(int id, UpdateKeyMetadataProductoRequest request)
        {
            var productoExistente = _productoRepository.GetById(id);

            if (productoExistente == null)
            {
                return false;
            }

            productoExistente.Nombre = request.Nombre ?? productoExistente.Nombre;
            productoExistente.Precio = request.Precio ?? productoExistente.Precio;
            productoExistente.Stock = request.Stock ?? productoExistente.Stock;

            return _productoRepository.UpdateKeyMetadata(productoExistente);
        }
    }
}
