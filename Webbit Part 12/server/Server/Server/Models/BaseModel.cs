using MongoDB.Bson;

namespace Server.Models
{
    public abstract class BaseModel
    {
        public ObjectId Id { get; set; }
    }
}