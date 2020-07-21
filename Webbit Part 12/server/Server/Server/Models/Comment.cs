using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Server.Models
{
    public class Comment : BaseContentItem, IVotable
    {
        public IList<ObjectId> Upvoters { get; set; }
        public IList<ObjectId> Downvoters { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public Comment(string content, ObjectId authorId, DateTime date)
            : base(content, authorId, date)
        {
            Id = ObjectId.GenerateNewId();
            Upvoters = new List<ObjectId>();
            Downvoters = new List<ObjectId>();
            Comments = new List<Comment>();
        }
    }
}