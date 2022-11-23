using Microsoft.Extensions.DependencyInjection;
using RC.MessageBus.RabbitMq;

namespace RC.MessageBus.Configuration
{
    public static class MessageBusConfiguration
    {
        public static IServiceCollection AddRabbitMqMessageBus(this IServiceCollection services, string? connection, MessageBusProviderEnum? provider = null)
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

        public static IServiceCollection AddKafkaMessageBus(this IServiceCollection services, List<string> bootstrapServers)
        {
            if (bootstrapServers.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentNullException();
            }

            var servers = string.Empty;

            foreach (var bootstrapServer in bootstrapServers)
            {
                servers += $"{bootstrapServer},";
            }

            servers = servers.Remove(servers.Length - 1);

            services.AddSingleton<IKafkaMessageBus>(new KafkaMessageBus(servers));

            return services;
        }
    }
}
