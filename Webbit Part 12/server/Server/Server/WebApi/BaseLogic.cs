using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Models;
using Server.MongoDB;
using Server.MongoDB.Extensions;

namespace Server.WebApi
{
    public abstract class BaseLogic<TModel> where TModel : BaseModel
    {
        protected IMongoDatabase Database { get; }
        protected UpdateDefinitionBuilder<TModel> UpdateBuilder => Builders<TModel>.Update;

        protected BaseLogic()
        {
            Database = MongoConnection.Database;
        }

        protected IMongoCollection<TModel> GetCollection()
        {
            return Database.GetCollection<TModel>();
        }

        protected async Task<TModel> Get(ObjectId id)
        {
            return await GetCollection().Get(id);
        }

        protected async Task Create(TModel model)
        {
            await GetCollection().InsertOneAsync(model);
        }
    }
}