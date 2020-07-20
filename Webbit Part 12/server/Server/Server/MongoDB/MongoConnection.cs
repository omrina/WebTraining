using System.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Server.MongoDB
{
    public class MongoConnection
    {
        public static IMongoDatabase Database = ConnectToDatabase();

        private static IMongoDatabase ConnectToDatabase()
        {
            var convention = new ConventionPack {new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("camelCase", convention, type => true);

            return new MongoClient(ConfigurationManager.AppSettings["MongoURL"])
                .GetDatabase(ConfigurationManager.AppSettings["MongoDatabaseName"]);
        }
    }
}