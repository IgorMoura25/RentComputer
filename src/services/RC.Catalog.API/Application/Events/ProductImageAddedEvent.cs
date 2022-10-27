﻿using RC.Core.Messages;

namespace RC.Catalog.API.Application.Events
{
    public class ProductImageAddedEvent : MediatREvent
    {
        public long Id { get; set; }
        public Guid ProductImageGuid { get; set; }
        public Guid ProductUniversalId { get; private set; }
        public string? Path { get; private set; }

        public ProductImageAddedEvent(long id, Guid productImageGuid, Guid productUniversalId, string? path)
        {
            AggregateId = id;
            Id = id;
            ProductImageGuid = productImageGuid;
            ProductUniversalId = productUniversalId;
            Path = path;
        }
    }
}
