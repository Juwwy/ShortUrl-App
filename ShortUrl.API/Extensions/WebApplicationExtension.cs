


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

                #endregion

                #region Other services

                builder.Services.AddControllers();
                builder.Services.AddMemoryCache();
                //builder.Services.AddStackExchangeRedisCache(redisOptions =>
                //{
                //    string connection = builder.Configuration
                //    .GetConnectionString("Redis");

                //    redisOptions.Configuration = connection;
                //});
                // builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { "localhost:6379" }, AbortOnConnectFail = false, ConnectRetry = 5 , ConnectTimeout = 5000}));
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
