using Microsoft.Extensions.Options;
using _0effort_crm_api.Contracts;
using MongoDB.Driver;

namespace _0effort_crm_api.Core;

public class MongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        var settings = dbOptions.Value;

        _client = new MongoClient(settings.ConnectionString);
        _database = _client.GetDatabase(settings.DatabaseName);

        Console.WriteLine("Connected to Mongo Instance");
    }

    public IMongoClient Client => _client;
    public IMongoDatabase Database => _database;
}