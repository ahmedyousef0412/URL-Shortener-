


namespace URL_Shortener.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{

    public DbSet<Url> Urls { get; set; }
}
