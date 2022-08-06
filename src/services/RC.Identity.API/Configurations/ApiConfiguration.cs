namespace RC.Identity.API.Configurations
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            var dataBaseSettings = configuration.GetSection(nameof(DataBaseSettings)).Get<DataBaseSettings>();

            services
                .RegisterDataServices(dataBaseSettings)
                .AddIdentity();

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
