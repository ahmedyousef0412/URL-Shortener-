

namespace URL_Shortener.Services;

public class UrlShortenerService(ApplicationDbContext context) : IUrlShortenerService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<string> ShortenUrlAsync(string originalUrl, CancellationToken cancellationToken)
    {
        if (originalUrl == null || string.IsNullOrWhiteSpace(originalUrl))
            throw new ArgumentException("Original Url cannot be null or empty.", nameof(originalUrl));

        var existingUrl = await _context.Urls.FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl,cancellationToken);

        if (existingUrl is not null)
            return existingUrl.ShortenedUrl;

        
        var shortenedUrl = GenerateShortenedUrl();

        var url = new Url
        {
            OriginalUrl = originalUrl,
            ShortenedUrl = shortenedUrl
        };

        _context.Add(url);
        await _context.SaveChangesAsync(cancellationToken);
        return shortenedUrl;
    }
  
    public async Task<Url> GetOriginalUrlAsync(string shortenedUrl, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(shortenedUrl))

            throw new ArgumentException("Shortened URL cannot be null or empty.", nameof(shortenedUrl));
        

        try
        {
            var url = await _context.Urls.FirstOrDefaultAsync(u => u.ShortenedUrl == shortenedUrl, cancellationToken);


            return url;
        }
        catch (Exception ex)
        {
            
            throw new Exception("An error occurred while retrieving the original URL.", ex);
        }
    }

    private static string GenerateShortenedUrl()
    {
        var guid = Guid.NewGuid().ToString().Substring(0, 8);
        return guid;
    }
}
