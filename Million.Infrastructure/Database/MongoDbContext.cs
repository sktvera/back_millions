using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Million.Domain.Configuration;     
using Million.Domain.Entities;         

namespace Million.Infrastructure.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public IMongoDatabase Database => _database;

        public MongoDbContext(IOptions<MongoSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.Connection);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        
        public IMongoCollection<Property> Properties =>
            _database.GetCollection<Property>("Properties");
    }
}
