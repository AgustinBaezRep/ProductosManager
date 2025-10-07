namespace Contracts.Responses;

public class GoogleApiLibroVolumeInfo
{
    public string Title { get; set; } = string.Empty;
    public List<string> Authors { get; set; } = new();
    public string PublishedDate { get; set; } = string.Empty;
}
