using RC.Core.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace RC.Catalog.API.Domain
{
    public class ProductImage : Entity
    {
        public long ProductId { get; private set; }
        public Guid ProductUniversalId { get; set; }
        public string Path { get; private set; }

        // EF
        [NotMapped]
        public Product Product { get; private set; }

        protected ProductImage() { }

        public ProductImage(long productId, Guid productUniversalId, string path)
        {
            UniversalId = Guid.NewGuid();
            ProductId = productId;
            ProductUniversalId = productUniversalId;
            Path = path;

            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                throw new DomainException("Invalid path");
            }

            if (ProductId < 0 || ProductId == 0)
            {
                throw new DomainException("Invalid ProductId");
            }
        }
    }
}
