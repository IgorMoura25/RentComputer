using System.Text.Json;
using Confluent.Kafka;
using RC.Core.Messages.IntegrationEvents;

namespace RC.MessageBus.RabbitMq
{
    public class KafkaMessageBus : IKafkaMessageBus
    {
        private readonly string _bootstrapServers;

        public KafkaMessageBus(string bootstrapServers)
        {
            _bootstrapServers = bootstrapServers;
        }

        public async Task ProduceAsync<T>(string topic, T message) where T : IntegrationEvent
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };

            var payload = JsonSerializer.Serialize(message);

            var producer = new ProducerBuilder<string, string>(config).Build();

            var result = await producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = payload
            });

            await Task.CompletedTask;
        }

        // executeAfterConsumed: Assim que ele consumir a mensagem, qual função irá executar?
        public async Task ConsumeAsync<T>(string topic, Func<T?, Task> executeAfterConsumed, CancellationToken cancellation) where T : IntegrationEvent
        {
            var teste = Task.Factory.StartNew(async () =>
            {
                var config = new ConsumerConfig
                {
                    GroupId = "test-group",
                    BootstrapServers = _bootstrapServers,
                    EnableAutoCommit = false, // false == A aplicação que se encarregará de dizer que "LEU" a mensagem do kafka
                    EnablePartitionEof = true // true == O kafka avisa através da "" se a partição chegou ao fim
                };

                using var consumer = new ConsumerBuilder<string, string>(config).Build();

                consumer.Subscribe(topic);

                while (!cancellation.IsCancellationRequested)
                {
                    var result = consumer.Consume();

                    // Se a partição chegou ao fim, consome novamente
                    if (result.IsPartitionEOF)
                    {
                        continue;
                    }

                    // Se não, quer dizer que foi consumida uma mensagem de fato, então...

                    // Deserializo a mensagem
                    var message = JsonSerializer.Deserialize<T>(result.Message.Value);

                    // Executo a função que está aguardando a mensagem como parâmetro
                    await executeAfterConsumed(message);

                    // Digo ao kafka que a mensagem foi "LIDA"
                    consumer.Commit();
                }
            }, cancellation, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}