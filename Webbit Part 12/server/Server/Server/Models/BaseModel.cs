using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public abstract class BaseModel
    {
        public ObjectId Id { get; set; }
    }
}