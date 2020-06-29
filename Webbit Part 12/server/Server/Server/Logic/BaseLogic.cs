using MongoDB.Driver;
using Server.Models;

namespace Server.Logic
{
    public abstract class BaseLogic<TModel> where TModel : BaseModel
    {
        protected IMongoCollection<TModel> Collection { get; }

        protected BaseLogic()
        {
            Collection = new MongoConnection().GetCollection<TModel>(typeof(TModel).Name.ToLower());
        }
    }
}