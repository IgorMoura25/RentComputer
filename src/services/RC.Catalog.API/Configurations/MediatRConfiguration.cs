using MediatR;
using RC.MessageBus.Mediator;
using System.Reflection;

namespace RC.Catalog.API.Configurations
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection RegisterMediatR(this IServiceCollection services)
        {
            services.AddScoped<IMediatRHandler, MediatRHandler>();
            services.AddScoped<MediatREventList>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
