using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RC.Catalog.API.Data.DTO
{
    public class ProductDTO
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
