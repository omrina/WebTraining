using System.Collections.Generic;
using MongoDB.Bson;

namespace Server.Models
{
    public class Subwebbit : BaseModel
    {
        public ObjectId OwnerId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Thread> Threads { get; set; }
        public long SubscribersCount { get; set; }

        public Subwebbit(string ownerId, string name)
        {
            OwnerId = ObjectId.Parse(ownerId);
            Name = name;
            Threads = new List<Thread>();
        }
    }
}