using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Recam.RealEstate.API.Models;

namespace Recam.RealEstate.API
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<CaseHistory> CaseHistories => _database.GetCollection<CaseHistory>("CaseHistories");
        public IMongoCollection<UserActivityLog> UserActivityLogs => _database.GetCollection<UserActivityLog>("UserActivityLogs");
    }
}
