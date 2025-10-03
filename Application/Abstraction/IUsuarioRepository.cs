using Contracts.Requests;
using Domain.Entities;

namespace Application.Abstraction
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario? GetByEmailAndPassowrd(LoginRequest request);
    }
}
