using RC.Core.DomainObjects;

namespace RC.Catalog.API.Domain
{
    public class ProductImage : Entity
    {
        public Product Product { get; set; }
        public string Path { get; private set; }

        public ProductImage(string path)
        {
            Path = path;
        }
    }
}
