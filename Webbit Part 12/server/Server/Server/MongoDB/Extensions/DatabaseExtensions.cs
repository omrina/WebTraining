using MongoDB.Driver;
using Server.Models;

namespace Server.MongoDB.Extensions
{
    public static class DatabaseExtensions
    {
        public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase database)
            where T : BaseModel
        {
            return database.GetCollection<T>(typeof(T).Name.ToLower());
        }
    }
}