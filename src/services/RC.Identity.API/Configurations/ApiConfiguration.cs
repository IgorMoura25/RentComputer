using Microsoft.AspNetCore.HttpOverrides;
using RC.MessageBus;
using RC.MessageBus.Configuration;
using RC.WebAPI.Core.Auth;

namespace RC.Identity.API.Configurations
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            var dataBaseSettings = configuration.GetSection(nameof(DataBaseSettings)).Get<DataBaseSettings>();
            var messageBusSettings = configuration.GetSection(nameof(MessageBusSettings)).Get<MessageBusSettings>();
            services.Configure<JwtConfigurationOptions>(configuration.GetSection("JwtConfiguration"));

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services
                .AddSwaggerConfiguration()
                .RegisterDataServices(dataBaseSettings)
                .AddIdentity()
                .AddJwtSigningCryptographyConfiguration(JwtSigningCryptography.Rsa)
                .AddMessageBusOrDefault(messageBusSettings.IntegrationConnectionString, MessageBusProviderEnum.EasyNetQ)
                .AddMemoryCache()
                .AddDistributedRedisCache(options =>
                {
                    options.Configuration = "localhost:6379";
                    options.InstanceName = "Demo_RedisCache";
                });

            services.AddCors(options =>
            {
                options.AddPolicy("Development", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            return services;
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHsts();

            app.UseSwaggerConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
