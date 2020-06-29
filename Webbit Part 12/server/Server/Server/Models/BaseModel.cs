using MongoDB.Bson;

namespace Server.Models
{
    public class BaseModel
    {
        public ObjectId Id { get; set; }
    }
}