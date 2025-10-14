using Contracts.Responses;

namespace Application.Abstraction.ExternalServices;

public interface IGoogleBookApiService
{
    IEnumerable<LibroResponse> SearchBooks(string query, CancellationToken cancellationToken = default);
}
