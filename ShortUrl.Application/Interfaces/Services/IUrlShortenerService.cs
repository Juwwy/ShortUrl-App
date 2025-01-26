
namespace ShortUrl.Application.Interfaces.Services
{
    public interface IUrlShortenerService
    {
        ValueTask<ResponseModel<string>> ShortenUrlAsync(ShortenDataModel shorten);
        ValueTask<ResponseModel<string>> GetLongUrlAsync(string shortUrl);
        ValueTask<ResponseModel<dynamic>> GetAccessCountAsync(string shortUrl);
    }
}
