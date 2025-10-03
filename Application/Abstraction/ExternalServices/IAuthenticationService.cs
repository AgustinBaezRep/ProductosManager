using Contracts.Requests;

namespace Application.Abstraction.ExternalServices;

public interface IAuthenticationService
{
    string Login(LoginRequest request);
}
