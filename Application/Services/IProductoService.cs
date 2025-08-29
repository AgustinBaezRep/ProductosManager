using Contracts.Requests;
using Contracts.Responses;

namespace Application.Services
{
    public interface IProductoService
    {
        List<ProductoResponse> GetAll();
        List<ProductoResponse> Search(string? name, int? categoriaId, decimal? pMin, decimal? pMax);
        List<ProductoResponse> GetByValue(decimal valor);
        ProductoResponse? GetById(int id);
        int GetTotalProductos();
        bool AssociateCategory(int id, int categoriaId);
        bool DisassociateCategory(int id);
        bool Create(CreateProductoRequest request);
        bool Update(int id, UpdateProductoRequest request);
        bool UpdateKeyMetadata(int id, UpdateKeyMetadataProductoRequest producto);
        bool Delete(int id);
    }
}
