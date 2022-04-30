using MediatR;
using FluentValidation.Results;
using RC.Catalog.API.Application.Commands;
using RC.Catalog.API.Application.Events;
using RC.Core.Mediator;
using System.Reflection;

namespace RC.Catalog.API.Configurations
{
    public static class MediatRConfiguration
    {
        public static void RegisterMediatR(this IServiceCollection services)
        {
            services.AddScoped<IMediatRHandler, MediatRHandler>();

            // Commands
            services.AddScoped<IRequestHandler<AddProductCommand, ValidationResult>, ProductCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<ProductAddedEvent>, ProductEventHandler>();
            services.AddScoped<MediatREventList>();

            //services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
