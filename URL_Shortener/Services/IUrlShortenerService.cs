
namespace URL_Shortener.Services;

public interface IUrlShortenerService
{
    Task<string> ShortenUrlAsync(string originalUrl, CancellationToken cancellationToken = default);
    Task<Url> GetOriginalUrlAsync(string shortenedUrl , CancellationToken cancellationToken = default);
}
