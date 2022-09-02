using RC.WebAPI.Core.Auth;

namespace RC.Catalog.API.Configurations
{
    public static class ApiConfiguration
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.Configure<DataBaseSettings>(configuration.GetSection(nameof(DataBaseSettings)));
            services.Configure<JwtConfigurationSettings>(configuration.GetSection(nameof(JwtConfigurationSettings)));

            var dataBaseSettings = configuration.GetSection(nameof(DataBaseSettings)).Get<DataBaseSettings>();
            var jwtConfigurationSettings = configuration.GetSection(nameof(JwtConfigurationSettings)).Get<JwtConfigurationSettings>();

            services
                .RegisterServices()
                .RegisterMediatR()
                .RegisterApplicationServices()
                .RegisterDataServices(dataBaseSettings)
                .AddSwaggerConfiguration()
                .AddJwtConfiguration(jwtConfigurationSettings.RetrievalUrl, jwtConfigurationSettings.RequiredHttps);

            services.AddCors(options =>
            {
                options.AddPolicy("Development", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHsts();

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
