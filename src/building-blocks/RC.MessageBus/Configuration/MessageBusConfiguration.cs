using Microsoft.Extensions.DependencyInjection;
using RC.MessageBus.EasyNetQ;

namespace RC.MessageBus.Configuration
{
    public static class MessageBusConfiguration
    {
        public static IServiceCollection AddMessageBusOrDefault(this IServiceCollection services, string connection, MessageBusProviderEnum? provider = null)
        {
            if (string.IsNullOrEmpty(connection) || string.IsNullOrWhiteSpace(connection))
            {
                throw new ArgumentNullException();
            }

            switch (provider)
            {
                case MessageBusProviderEnum.EasyNetQ:
                default:
                    services.AddSingleton<IEasyNetQBus>(new EasyNetQMessageBus(connection));
                    break;
            }

            return services;
        }
    }
}
