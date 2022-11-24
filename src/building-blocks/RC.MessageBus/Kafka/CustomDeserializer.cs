using System.IO.Compression;
using System.Text.Json;
using Confluent.Kafka;

namespace RC.MessageBus.Kafka
{
    public class CustomDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            // Instancia um Stream de memória já com o dado compactado a ser deserializado
            using var memoryStream = new MemoryStream(data.ToArray());

            // Agora a instancia do GZip stream será no modo Decompress
            using var zipStream = new GZipStream(memoryStream, CompressionMode.Decompress, true);

            // Deserializa o dado já descomprimido
            return JsonSerializer.Deserialize<T>(zipStream);
        }
    }
}
