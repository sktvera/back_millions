using Million.Domain.Entities;
using Million.Infrastructure.Database;
using MongoDB.Driver;
using MongoDB.Bson; 

namespace Million.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoCollection<Property> _collection;

        public PropertyRepository(MongoDbContext context)
        {
            _collection = context.Properties;
        }

        public async Task<IEnumerable<Property>> GetAllAsync(
            string? name = null, string? address = null,
            decimal? minPrice = null, decimal? maxPrice = null)
        {
            var filter = Builders<Property>.Filter.Empty;

            if (!string.IsNullOrEmpty(name))
                filter &= Builders<Property>.Filter.Regex(x => x.Name, new BsonRegularExpression(name, "i"));

            if (!string.IsNullOrEmpty(address))
                filter &= Builders<Property>.Filter.Regex(x => x.Address, new BsonRegularExpression(address, "i"));

            if (minPrice.HasValue)
                filter &= Builders<Property>.Filter.Gte(x => x.Price, minPrice.Value);

            if (maxPrice.HasValue)
                filter &= Builders<Property>.Filter.Lte(x => x.Price, maxPrice.Value);

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Property property) =>
            await _collection.InsertOneAsync(property);

        public async Task UpdateAsync(string id, Property property) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, property);

        public async Task DeleteAsync(string id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
