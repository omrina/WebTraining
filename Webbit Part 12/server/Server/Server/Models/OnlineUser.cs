using MongoDB.Bson;

namespace Server.Models
{
    // TODO: to inherit from baseModel or to not? (rn yes)
    public class OnlineUser : BaseModel
    {
        public ObjectId Token { get; set; }

        public OnlineUser(ObjectId id)
        {
            Id = id;
            Token = ObjectId.GenerateNewId();
        }
    }
}