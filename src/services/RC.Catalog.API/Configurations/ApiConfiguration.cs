using RC.MessageBus;
using RC.MessageBus.Configuration;

namespace RC.Catalog.API.Configurations
{
    public static class ApiConfiguration
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.Configure<DataBaseSettings>(configuration.GetSection(nameof(DataBaseSettings)));
            services.Configure<MessageBusSettings>(configuration.GetSection(nameof(MessageBusSettings)));

            var dataBaseSettings = configuration.GetSection(nameof(DataBaseSettings)).Get<DataBaseSettings>();
            var messageBusSettings = configuration.GetSection(nameof(MessageBusSettings)).Get<MessageBusSettings>();

            services
                .RegisterServices()
                .RegisterApplicationServices()
                .RegisterDataServices(dataBaseSettings)
                .AddMessageBusOrDefault(messageBusSettings.ConnectionString, MessageBusProviderEnum.EasyNetQ)
                .AddSwaggerConfiguration();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
