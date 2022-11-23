using RC.Customer.API.Services;
using RC.MessageBus;
using RC.MessageBus.Configuration;

namespace RC.Customer.API.Configurations
{
    public static class ApiConfiguration
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.RegisterServices(configuration);

            var messageBusSettings = configuration.GetSection(nameof(MessageBusSettings)).Get<MessageBusSettings>();

            services.AddHostedService<CustomerIntegrationHandler>();

            services.AddRabbitMqMessageBus(messageBusSettings.IntegrationConnectionString, MessageBusProviderEnum.EasyNetQ);

            services.AddKafkaMessageBus(new List<string>() { { "localhost:9092" } });

            services.RegisterMediatR();

            services.AddSwaggerConfiguration();
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
