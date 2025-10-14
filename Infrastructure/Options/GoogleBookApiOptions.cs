namespace Infrastructure.Options;

public class GoogleBookApiOptions
{
    public const string SectionName = "BookApi";
    public string BaseUrl { get; set; } = string.Empty;
}
