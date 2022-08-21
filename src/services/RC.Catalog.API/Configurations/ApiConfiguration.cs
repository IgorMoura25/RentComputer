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
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
