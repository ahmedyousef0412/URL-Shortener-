namespace URL_Shortener.Entities;

public class Url
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; } = null!;
    public string ShortenedUrl { get; set; } = null!;
}
