using MongoDB.Driver;

namespace Server.MongoDB.Extensions
{
    public static class DatabaseExtensions
    {
        public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase database)
        {
            return database.GetCollection<T>(typeof(T).Name.ToLower());
        }
    }
}