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
                
                var isDev = builder.Configuration["Environment"] == "Development";
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c => c.ResolveConflictingActions(apides => apides.First()));

                #endregion

                #region Other services

                builder.Services.AddControllers();

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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
