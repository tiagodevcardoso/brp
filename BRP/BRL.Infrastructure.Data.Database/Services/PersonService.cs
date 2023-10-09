using BRL.Infrastructure.Data.Models;
using BRL.Infrastructure.Models.Base.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BRL.Infrastructure.Data.Services
{
    public class PersonService
    {
        private readonly IMongoCollection<Person> _personCollection;

        public PersonService(IOptions<MongoDBSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _personCollection = mongoDatabase.GetCollection<Person>(
                databaseSettings.Value.CollectionName);
        }

        public async Task<List<Person>> GetAsync() =>
            await _personCollection.Find(_ => true).ToListAsync();

        public async Task<Person?> GetAsync(string id) =>
            await _personCollection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();

        public async Task CreateAsync(Person newPerson) =>
            await _personCollection.InsertOneAsync(newPerson);

        public async Task UpdateAsync(string id, Person updatedPerson) =>
            await _personCollection.ReplaceOneAsync(x => x.Id == ObjectId.Parse(id), updatedPerson);

        public async Task RemoveAsync(string id) =>
            await _personCollection.DeleteOneAsync(x => x.Id == ObjectId.Parse(id));
    }
}