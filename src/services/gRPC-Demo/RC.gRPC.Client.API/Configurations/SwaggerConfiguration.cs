﻿using Microsoft.OpenApi.Models;

namespace RC.gRPC.Client.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "RentComputer Enterprise gRPC Client API",
                    Description = "This is the RentComputer gRPC Client API",
                    Contact = new OpenApiContact()
                    {
                        Name = "Igor Moura",
                        Email = "igor.moura254@hotmail.com"
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
            });

            return services;
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
