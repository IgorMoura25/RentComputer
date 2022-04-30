using System.Reflection;
using MediatR;
using RC.Core.Mediator;

namespace RC.Customer.API.Configurations
{
    public static class MediatRConfiguration
    {
        public static void RegisterMediatR(this IServiceCollection services)
        {
            services.AddScoped<IMediatRHandler, MediatRHandler>();
            services.AddScoped<MediatREventList>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
