
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace ShortUrl.API.Middlewares
{

    public class RateLimitMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IMemoryCache cache;
        private const int Limit = 100; // Max requests per minute
        private const int TimeWindowInSeconds = 60;

        public RateLimitMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            this.next = next;
            this.cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(clientIp))
            {
                await next(context);
                return;
            }

            var cacheKey = $"RateLimit:{clientIp}";
            var requestCount = IncrementRequestCount(cacheKey);

            if (requestCount > Limit)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await context.Response.WriteAsync("Request rate limit exceeded. Try again later.");
                return;
            }

            await next(context);
        }

        private int IncrementRequestCount(string cacheKey)
        {
            var requestCount = cache.Get<int>(cacheKey);

            if (requestCount == 0)
            {
                cache.Set(cacheKey, 1, TimeSpan.FromSeconds(TimeWindowInSeconds));
                return 1;
            }

            cache.Set(cacheKey, ++requestCount, TimeSpan.FromSeconds(TimeWindowInSeconds));
            return requestCount;
        }
    }
}
