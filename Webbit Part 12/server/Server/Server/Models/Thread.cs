using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Server.Models
{
    public class Thread : BaseContentItem, IVotable
    {
        public string Title { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IList<ObjectId> Upvoters { get; set; }
        public IList<ObjectId> Downvoters { get; set; }

        public Thread(string content, ObjectId authorId, DateTime date, string title) 
            : base(content, authorId, date)
        {
            Title = title;
            Comments = new List<Comment>();
            Upvoters = new List<ObjectId>();
            Downvoters = new List<ObjectId>();
        }
    }
}