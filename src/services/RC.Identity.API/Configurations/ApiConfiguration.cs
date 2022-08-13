using RC.WebAPI.Core.Auth;

namespace RC.Identity.API.Configurations
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            var dataBaseSettings = configuration.GetSection(nameof(DataBaseSettings)).Get<DataBaseSettings>();
            services.Configure<JwtConfigurationOptions>(configuration.GetSection("JwtConfiguration"));

            services
                .AddSwaggerConfiguration()
                .RegisterDataServices(dataBaseSettings)
                .AddIdentity()
                .AddJwtSigningCryptographyConfiguration(JwtSigningCryptography.Rsa);

            return services;
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();

            app.UseSwaggerConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
