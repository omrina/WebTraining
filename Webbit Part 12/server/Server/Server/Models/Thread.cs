using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class Thread : BaseModel, IVotable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        // TODO: add comments!!
        public IEnumerable<ObjectId> Upvoters { get; set; }
        public IEnumerable<ObjectId> Downvoters { get; set; }
        public int Rating { get; set; }
        // public int Rating => Upvoters.Count() - Downvoters.Count();

        public Thread(string title, string content, string author)
        {
            Id = ObjectId.GenerateNewId();
            Title = title;
            Content = content;
            Author = author;
            Date = DateTime.Now;
            Upvoters = new List<ObjectId>();
            Downvoters = new List<ObjectId>();
        }
    }
}