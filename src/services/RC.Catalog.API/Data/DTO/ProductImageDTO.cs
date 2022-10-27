using MongoDB.Bson.Serialization.Attributes;

namespace RC.Catalog.API.Data.DTO
{
    public class ProductImageDTO
    {
        [BsonId]
        public string? Id { get; set; }
        public string? ProductImageGuid { get; set; }
        public string? ProductUniversalId { get; set; }
        public string? Path { get; set; }
    }
}
