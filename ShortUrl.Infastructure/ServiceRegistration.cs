
namespace ShortUrl.Infastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUrlShortenerService, UrlShortenerService>();
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                string connection = configuration
                .GetConnectionString("Redis");

                redisOptions.Configuration = connection;
            });

            return services;
        }
    }
}
