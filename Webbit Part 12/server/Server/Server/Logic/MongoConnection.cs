using System.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Server.Models;

namespace Server.Logic
{
    public class MongoConnection
    {
        public static IMongoDatabase Database = ConnectToDatabase();

        private static IMongoDatabase ConnectToDatabase()
        {
            var convention = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", convention, type => true);

            return new MongoClient(ConfigurationManager.AppSettings["MongoURL"])
                .GetDatabase(ConfigurationManager.AppSettings["MongoDatabaseName"]);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) where T : BaseModel
        {
            return Database.GetCollection<T>(collectionName);
        }
    }
}