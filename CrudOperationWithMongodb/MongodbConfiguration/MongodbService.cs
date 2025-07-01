using CrudOperationWithMongodb.Models;
using MongoDB.Driver;

namespace CrudOperationWithMongodb.MongodbConfiguration
{
    public class MongodbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _client;

        public MongodbService(IConfiguration configuration, IMongoClient client)
        {
            _configuration = configuration;
            _client = client;
            _database = _client.GetDatabase(_configuration["ConnectionStrings:databaseName"]);
        }

        //collections

        public IMongoCollection<InsertRecordRequest> User =>
            _database.GetCollection<InsertRecordRequest>("UserDetails");

    }
}
