using Application.Abstraction.ExternalServices;
using Contracts.Responses;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Infrastructure.ExternalServices;

public class GoogleBookApiService : IGoogleBookApiService
{
    private readonly HttpClient _httpClient;

    public GoogleBookApiService(HttpClient httpClient, IOptions<GoogleBookApiOptions> options)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
    }

    public IEnumerable<LibroResponse> SearchBooks(string query, CancellationToken cancellationToken = default)
    {
        // Llamada a la api de Google
        var response = _httpClient.GetFromJsonAsync<GoogleApiLibroResponse>
            ($"volumes?q={Uri.EscapeDataString(query)}", cancellationToken).Result;

        // Mapear la respuesta al formato deseado
        return response?.Items?.Select(i => new LibroResponse
        {
            Title = i.VolumeInfo.Title,
            Authors = i.VolumeInfo.Authors,
            PublishedDate = i.VolumeInfo.PublishedDate
        }) ?? Enumerable.Empty<LibroResponse>();
    }
}
