using Domain.Entities;

namespace Application.Abstraction
{
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
        bool CheckIfIsAssociatedToAnyProduct(int categoriaId);
    }
}
