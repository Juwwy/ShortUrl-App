


using Microsoft.OpenApi.Models;

namespace ShortUrl.API.Extensions
{
    public static class WebApplicationExtension
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            try
            {
                #region CORs
                builder.Services.AddCors(opt =>
                {
                    opt.AddPolicy(name: "allowAllOrigins", policy =>
                    {
                        policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
                });

                #endregion

                #region Swagger
                
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c => c.ResolveConflictingActions(apides => apides.First()));
                builder.Services.AddSwaggerGen(c =>
                {

                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "URL Shortener API",
                        License = new OpenApiLicense
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });
                    

                });

                #endregion

                #region Other services

                builder.Services.AddControllers();
                builder.Services.AddMemoryCache();
                builder.Services.AddInfrastructureServices(builder.Configuration);
                builder.Services.AddApplicationServices();
                builder.Services.AddRepositoryServices(builder.Configuration);
                builder.Services.AddRepositories();

                #endregion

                return builder.Build();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseGlobalException();
            app.UseHttpLogging();
            // Add rate-limiting middleware
            app.UseMiddleware<RateLimitMiddleware>();
            app.UseRouting();
            app.UseCors("allowAllOrigins");
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
