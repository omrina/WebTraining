using System.Collections.Generic;
using MongoDB.Bson;

namespace Server.Models
{
    public interface IVotable
    {
        IList<ObjectId> Upvoters { get; set; }
        IList<ObjectId> Downvoters { get; set; }
    }
}