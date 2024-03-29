﻿using MediatR;
using RC.Catalog.API.Data.DTO;
using RC.Catalog.API.Data.Repositories;

namespace RC.Catalog.API.Application.Events
{
    public class ProductEventHandler :
        INotificationHandler<ProductAddedEvent>,
        INotificationHandler<ProductImageAddedEvent>
    {
        private readonly IProductQueryRepository _productQueryRepository;

        public ProductEventHandler(IProductQueryRepository productQueryRepository)
        {
            _productQueryRepository = productQueryRepository;
        }

        public async Task Handle(ProductAddedEvent notification, CancellationToken cancellationToken)
        {
            await _productQueryRepository.CreateAsync(new ProductDTO()
            {
                Id = Guid.NewGuid().ToString(),
                ProductGuid = notification.ProductGuid.ToString(),
                Name = notification.Name,
                Description = notification.Description,
                Value = notification.Value,
                Quantity = notification.Quantity,
                IsActive = notification.IsActive,
                CreatedAt = notification.CreatedAt
            });
        }

        public async Task Handle(ProductImageAddedEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productQueryRepository.GetByGuidAsync(notification.ProductUniversalId);

            if (product == null) return;

            if (product.Images == null) product.Images = new List<ProductImageDTO>();

            product.Images?.Add(new ProductImageDTO()
            {
                Id = Guid.NewGuid().ToString(),
                ProductImageGuid = notification.ProductImageGuid.ToString(),
                ProductUniversalId = notification.ProductUniversalId.ToString(),
                ImageName = notification.ImageName,
                Path = notification.Path
            });

            await _productQueryRepository.UpdateByGuidAsync(notification.ProductUniversalId, product);
        }
    }
}
