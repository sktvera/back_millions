using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Million.Domain.Configuration;     // MongoSettings
using Million.Domain.Entities;         // <- IMPORTANTE

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

        // <- Habilita esta propiedad para que el repo funcione
        public IMongoCollection<Property> Properties =>
            _database.GetCollection<Property>("Properties");
    }
}
