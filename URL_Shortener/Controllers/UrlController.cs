
namespace URL_Shortener.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UrlController(IUrlShortenerService urlShortenerService) : ControllerBase
{
    private readonly IUrlShortenerService _urlShortenerService = urlShortenerService;

    [HttpPost]
    [Route("shorten")]
    public async Task< IActionResult> ShortenUrl([FromBody] ShortenUrlRequest request , CancellationToken cancellationToken)
    {

        var shortenedUrl = await _urlShortenerService.ShortenUrlAsync(request.OriginalUrl, cancellationToken);

        return Ok(new { ShortenedUrl = shortenedUrl });
    }


    [HttpGet]
    [Route("{shortenedUrl}")]
    public async Task<IActionResult> RedirectToOriginal(string shortenedUrl,CancellationToken cancellationToken)
    {
        var url = await _urlShortenerService.GetOriginalUrlAsync(shortenedUrl,cancellationToken);

        return url is null ? NotFound() : Redirect(url.OriginalUrl);

       
    }
}
