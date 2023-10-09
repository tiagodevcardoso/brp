using BRL.Infrastructure.Data.Models;
using ParkBee.MongoDb;

namespace BRL.Infrastructure.Data
{
    public class MongoDBContext : MongoContext
    {
        public MongoDBContext(IMongoContextOptionsBuilder optionsBuilder) : base(optionsBuilder) { }

        public DbSet<Person> Person { set; get; }
    }
}