using MediatR;
using FluentValidation.Results;
using RC.Core.Mediator;
using System.Reflection;
using RC.Customer.API.Application.Commands;
using RC.Customer.API.Application.Events;

namespace RC.Customer.API.Configurations
{
    public static class MediatRConfiguration
    {
        public static void RegisterMediatR(this IServiceCollection services)
        {
            services.AddScoped<IMediatRHandler, MediatRHandler>();

            // Commands
            services.AddScoped<IRequestHandler<AddCustomerCommand, ValidationResult>, CustomerCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<CustomerAddedEvent>, CustomerEventHandler>();
            services.AddScoped<MediatREventList>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
