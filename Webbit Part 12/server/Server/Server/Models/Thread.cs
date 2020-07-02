using System;
using MongoDB.Bson;

namespace Server.Models
{
    public class Thread : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        // TODO: to local serialization?
        public DateTime Date { get; set; }
        // TODO: add rating AND comments!!

        public Thread(string title, string content, string author)
        {
            Id = ObjectId.GenerateNewId();
            Title = title;
            Content = content;
            Author = author;
            Date = DateTime.Now;
        }
    }
}