using System.IO.Compression;
using System.Text.Json;
using Confluent.Kafka;

namespace RC.MessageBus.Kafka
{
    public class CustomSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            // Converte para bytes
            var bytes = JsonSerializer.SerializeToUtf8Bytes(data);

            // Instancia um GZip stream com base um Stream em memória
            using var memoryStream = new MemoryStream();
            using var zipStream = new GZipStream(memoryStream, CompressionMode.Compress, true);

            // Escreve os bytes do dado na memória usando a compressão do GZip
            zipStream.Write(bytes, 0, bytes.Length);
            zipStream.Close();

            // Agora que os dados estão comprimidos na memória, converte para um array de bytes
            var buffer = memoryStream.ToArray();

            return buffer;
        }
    }
}
