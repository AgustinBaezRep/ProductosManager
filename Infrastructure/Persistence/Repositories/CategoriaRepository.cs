using Application.Abstraction;
using Contracts.Requests;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public CategoriaRepository() { }

        public List<Categoria> GetAll()
        {
            return DataTables.Categorias.ToList();
        }

        public Categoria? GetById(int id)
        {
            return DataTables.Categorias.FirstOrDefault(x => x.Id == id);
        }

        public bool Create(Categoria categoria)
        {
            DataTables.Categorias.Add(categoria);

            return true;
        }

        public bool Update(Categoria categoria, UpdateCategoriaRequest request)
        {
            categoria.Nombre = request.Nombre;

            return true;
        }

        public bool Delete(Categoria categoria)
        {
            return DataTables.Categorias.Remove(categoria);
        }

        public List<Categoria> GetByCriteria(Expression<Func<Categoria, bool>> expression)
        {
            return DataTables.Categorias.AsQueryable().Where(expression).ToList();
        }

        public bool CheckIfIsAssociatedToAnyProduct(int categoriaId)
        {
            return DataTables.Productos.Any(p => p.Categoria?.Id == categoriaId);
        }
    }
}
