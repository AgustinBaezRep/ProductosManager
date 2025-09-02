using Contracts.Requests;
using Contracts.Responses;

namespace Application.Services
{
    public interface ICategoriaService
    {
        List<CategoriaResponse> GetAll();
        CategoriaResponse? GetById(int id);
        List<ProductoResponse> GetProductosByCategoriaId(int id);
        bool Create(CreateCategoriaRequest request);
        bool Update(int id, UpdateCategoriaRequest request);
        bool Delete(int id);
    }
}
