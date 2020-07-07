using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public interface IVotable
    {
        IEnumerable<ObjectId> Upvoters { get; set; }
        IEnumerable<ObjectId> Downvoters { get; set; }
    }
}