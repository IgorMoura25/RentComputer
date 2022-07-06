﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RC.Catalog.API.Data.DTO
{
    public class ProductDTO
    {
        [BsonId]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
    }
}
