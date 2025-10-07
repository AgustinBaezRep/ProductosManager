namespace Contracts.Responses;

public class LibroResponse
{
    public string Title { get; set; } = string.Empty;
    public IEnumerable<string> Authors { get; set; } = [];
    public string PublishedDate { get; set; } = string.Empty;
}
