using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Models;

namespace Server.Logic
{
    public abstract class BaseLogic<TModel> where TModel : BaseModel
    {
        protected IMongoCollection<TModel> Collection { get; }

        protected Expression<Func<TModel, bool>> GenerateByIdFilter(string id)
        {
            return x => x.Id == new ObjectId(id);
        }

        protected BaseLogic()
        {
            Collection = new MongoConnection().GetCollection<TModel>(typeof(TModel).Name.ToLower());
        }

        protected IMongoQueryable<TModel> GetAll()
        {
            return Collection.AsQueryable();
        }

        protected IMongoQueryable<TModel> Get(string id)
        {
            return GetAll().Where(GenerateByIdFilter(id));
        }

        // TODO: add more functions ()
    }
}