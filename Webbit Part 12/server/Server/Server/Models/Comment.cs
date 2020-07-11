using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Server.Models
{
    public class Comment : BaseModel, IVotable
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<ObjectId> Upvoters { get; set; }
        public IEnumerable<ObjectId> Downvoters { get; set; }
        public int Rating { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public Comment(string author, string content)
        {
            Id = ObjectId.GenerateNewId();
            Author = author;
            Content = content;
            Date = DateTime.Now;
            Upvoters = new List<ObjectId>();
            Downvoters = new List<ObjectId>();
            Comments = new List<Comment>();
        }
    }
}