using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Exceptions;
using Server.Models;

namespace Server.MongoDB.Extensions
{
    public static class CollectionExtensions
    {
        public static async Task<T> Get<T>(this IMongoCollection<T> collection, ObjectId id)
            where T : BaseModel
        {
            var model = await collection.AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                throw new ModelNotFoundException();
            }

            return model;
        }
    }
}