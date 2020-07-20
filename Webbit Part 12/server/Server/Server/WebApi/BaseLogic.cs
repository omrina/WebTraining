using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Exceptions;
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

        // TODO: move to extension? (maybe get all as well?)
        protected async Task<TModel> Get(ObjectId id)
        {
            var model = await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                throw new ModelNotFoundException();
            }

            return model;
        }
    }
}