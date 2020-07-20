using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    // TODO: to inherit from baseModel or to not?
    public class OnlineUser : BaseModel
    {
        // [BsonId]
        public ObjectId Token { get; set; }

        public OnlineUser(ObjectId id)
        {
            Id = id;
            Token = ObjectId.GenerateNewId();
        }
    }
}