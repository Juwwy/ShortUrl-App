
namespace ShortUrl.Repository.Repositories
{
    public class UrlShortenerRepository : GenericRepository<UrlShortener>, IUrlShortenerRepository
    {
        public UrlShortenerRepository(ShortUrlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
