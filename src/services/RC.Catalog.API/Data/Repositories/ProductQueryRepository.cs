using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RC.Catalog.API.Configurations;
using RC.Catalog.API.Data.DTO;

namespace RC.Catalog.API.Data.Repositories
{
    public class ProductQueryRepository : IProductQueryRepository
    {
        private readonly IMongoCollection<ProductDTO> _productsCollection;

        public ProductQueryRepository(IOptions<DataBaseSettings> dataBaseSettings)
        {
            var mongoClient = new MongoClient(dataBaseSettings.Value.ReadConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dataBaseSettings.Value.ReadDatabaseName);

            _productsCollection = mongoDatabase.GetCollection<ProductDTO>("Products");
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            return await _productsCollection.Find(x => true).ToListAsync();

        }

        public async Task<ProductDTO?> GetByNameAsync(string name)
        {
            return await _productsCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(ProductDTO product)
        {
            await _productsCollection.InsertOneAsync(product);
        }

        public void Dispose()
        {
        }
    }
}
