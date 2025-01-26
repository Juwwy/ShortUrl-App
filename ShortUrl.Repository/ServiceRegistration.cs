
namespace ShortUrl.Repository
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
           

            services.AddDbContext<ShortUrlDbContext>((sp, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ShortenUrlConnection"));
                options.EnableSensitiveDataLogging(true);
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork>(c => { return c.GetRequiredService<ShortUrlDbContext>(); });
            services.AddScoped<IUrlShortenerRepository, UrlShortenerRepository>();
            
            return services;
        }
    }
}
