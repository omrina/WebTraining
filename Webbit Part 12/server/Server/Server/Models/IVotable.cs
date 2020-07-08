using System.Collections.Generic;
using MongoDB.Bson;

namespace Server.Models
{
    public interface IVotable
    {
        IEnumerable<ObjectId> Upvoters { get; set; }
        IEnumerable<ObjectId> Downvoters { get; set; }
    }
}