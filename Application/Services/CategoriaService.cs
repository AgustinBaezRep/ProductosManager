using Application.Abstraction;
using Contracts.Requests;
using Contracts.Responses;
using Domain.Entities;
using System.Data;

namespace Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProductoRepository _productoRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository, IProductoRepository productoRepository)
        {
            _categoriaRepository = categoriaRepository;
            _productoRepository = productoRepository;
        }

        public List<CategoriaResponse> GetAll()
        {
            var listaCategorias = _categoriaRepository
                .GetAll()
                .Select(s => new CategoriaResponse()
                {
                    Id = s.Id,
                    Nombre = s.Nombre
                }).ToList();

            return listaCategorias;
        }

        public CategoriaResponse? GetById(int id)
        {
            var response = _categoriaRepository.GetById(id) is Categoria categoria ?
                new CategoriaResponse()
                {
                    Id = categoria.Id,
                    Nombre = categoria.Nombre
                } : null;

            return response;
        }

        public List<ProductoResponse> GetProductosByCategoriaId(int id)
        {
            var categoria = _categoriaRepository.GetById(id);

            if (categoria == null)
            {
                return new List<ProductoResponse>();
            }

            var productos = _productoRepository
                .GetAll()
                .Where(p => p.Categoria?.Id == id)
                .Select(p => new ProductoResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    Categoria = categoria
                })
                .ToList();

            return productos;
        }

        public bool Create(CreateCategoriaRequest request)
        {
            var listaCategorias = _categoriaRepository.GetAll();

            if (request == null || string.IsNullOrWhiteSpace(request.Nombre))
            {
                return false;
            }

            var nuevaCategoria = new Categoria
            {
                Id = listaCategorias.Any() ? listaCategorias.Max(x => x.Id) + 1 : 1,
                Nombre = request.Nombre
            };

            return _categoriaRepository.Create(nuevaCategoria);
        }

        public bool Update(int id, UpdateCategoriaRequest request)
        {
            var categoriaExistente = _categoriaRepository.GetById(id);

            if (categoriaExistente == null)
            {
                return false;
            }

            if (request == null || string.IsNullOrWhiteSpace(request.Nombre))
            {
                return false;
            }

            if (_categoriaRepository.CheckIfIsAssociatedToAnyProduct(id))
            {
                return false;
            }

            categoriaExistente.Nombre = request.Nombre;

            return _categoriaRepository.Update(categoriaExistente);
        }

        public bool Delete(int id)
        {
            var categoriaExistente = _categoriaRepository.GetById(id);

            if (categoriaExistente == null)
            {
                return false;
            }

            if (_categoriaRepository.CheckIfIsAssociatedToAnyProduct(id))
            {
                return false;
            }

            return _categoriaRepository.Delete(categoriaExistente);
        }
    }
}
