

namespace ShortUrl.Domain.Entities
{
    public class UrlShortener:AuditableEntity
    {
        public int UrlShortenerId { get; set; }
        public string ShortUrl { get; set; }
        public string LongUrl { get; set; }
        public int RequestCount { get; set; }
    }
}
