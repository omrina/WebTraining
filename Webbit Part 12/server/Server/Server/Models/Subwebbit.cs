using System.Collections.Generic;
using MongoDB.Bson;

namespace Server.Models
{
    public class Subwebbit : BaseModel
    {
        public ObjectId OwnerId { get; set; }
        public string Name { get; set; }
        public IEnumerable<ObjectId> Threads { get; set; }
        public long SubscribersCount { get; set; }

        public Subwebbit(ObjectId ownerId, string name)
        {
            OwnerId = ownerId;
            Name = name;
            Threads = new List<ObjectId>();
        }
    }
}