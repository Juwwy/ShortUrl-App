
namespace ShortUrl.Infastructure.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUrlShortenerRepository urlShortenerRepository;
        private readonly IMemoryCache cache;
        private readonly ILogger<UrlShortenerService> logger;
        private readonly IValidator<ShortenDataModel> shortenValidator;

        public UrlShortenerService(IUnitOfWork unitOfWork, IUrlShortenerRepository urlShortenerRepository, IMemoryCache cache, ILogger<UrlShortenerService> logger,
            IValidator<ShortenDataModel> shortenValidator)
        {
            this.unitOfWork = unitOfWork;
            this.urlShortenerRepository = urlShortenerRepository;
            this.cache = cache;
            this.logger = logger;
            this.shortenValidator = shortenValidator;
        }
        public async ValueTask<ResponseModel<dynamic>> GetAccessCountAsync(string shortUrl)
        {
            try
            {
                logger.LogInformation($"Request to Get AccessCount with Url: [{shortUrl}]");

                var urlShortener = await urlShortenerRepository.FindAll()
                    .FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
                return ResponseModel<dynamic>.SuccessMessage("success", data: urlShortener?.RequestCount ?? 0);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Error response GET AccessCount with url '{shortUrl}' ==> Message: [{ex.Message}]");
                return ResponseModel<dynamic>.ErrorMessage("Error fetching accessCount. Please try again.");
            }
        }

        public async ValueTask<ResponseModel<string>> GetLongUrlAsync(string shortUrl)
        {
            try
            {
                logger.LogInformation($"Request to retrieve original url with ShortUrl: [{shortUrl}]");

                if (cache.TryGetValue(shortUrl, out string cachedUrl))
                    return ResponseModel<string>.SuccessMessage("success", cachedUrl);

                var urlShortener = await urlShortenerRepository.FindAll()
                    .FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
                if (urlShortener == null) return null;

                urlShortener.RequestCount++;
                await unitOfWork.SaveChangesAsync();

                cache.Set(shortUrl, urlShortener.LongUrl, TimeSpan.FromMinutes(10));

                return ResponseModel<string>.SuccessMessage("success", urlShortener.LongUrl);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Error response GET ShortUrl: [{ex.Message}]");
                return ResponseModel<string>.ErrorMessage("Error fetching url. Please try again.");

            }
        }

        public async ValueTask<ResponseModel<string>> ShortenUrlAsync(ShortenDataModel shorten)
        {
            try
            {
                logger.LogInformation($"Request to convert longUrl: [{shorten.LongUrl}]");

                var validatorResult = await shortenValidator.ValidateAsync(shorten);

                if (validatorResult.Errors.Count > 0)
                    return ResponseModel<string>.ErrorMessage(validatorResult.Errors.Select(x => x.ErrorMessage).First());


                var existingShortUrl = await urlShortenerRepository.FindAll()
                .FirstOrDefaultAsync(u => u.LongUrl == shorten.LongUrl);

                if (existingShortUrl != null)
                    return ResponseModel<string>.SuccessMessage("success", existingShortUrl.ShortUrl);

                var newShortUrl = $"{GenerateShortUrlHelper.GenerateShortUrl(5)}.ly";

                while (await urlShortenerRepository.FindAll().AnyAsync(x => string.Equals(newShortUrl, x.ShortUrl)))
                {
                    newShortUrl = $"{GenerateShortUrlHelper.GenerateShortUrl(5)}.ly";
                }


                var urlShortener = new UrlShortener
                {
                    LongUrl = shorten.LongUrl,
                    ShortUrl = newShortUrl
                };

                await urlShortenerRepository.AddAsync(urlShortener, CancellationToken.None);
                await unitOfWork.SaveChangesAsync();
                return ResponseModel<string>.SuccessMessage("success", urlShortener.ShortUrl);
            }
            catch (Exception ex )
            {
                logger.LogInformation($"Error response POST ShortUrl: [{ex.Message}]");
                return ResponseModel<string>.ErrorMessage("Error occurred while making request. Please try again.");
            }
        }
    }
}
