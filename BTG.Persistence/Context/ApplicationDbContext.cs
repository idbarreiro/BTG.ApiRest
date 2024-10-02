using BTG.Domain.Entities;
using BTG.Persistence.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BTG.Persistence.Context
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase _database;

        public ApplicationDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        #region Collections
        public IMongoCollection<Fund> GetCollectionFund<T>(string name)
        {
            return _database.GetCollection<Fund>(name);
        }

        public IMongoCollection<Transaction> GetCollectionTransaction<T>(string name)
        {
            return _database.GetCollection<Transaction>(name);
        }
        #endregion

    }
}
