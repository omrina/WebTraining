using MongoDB.Bson;

namespace Server.Models
{
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