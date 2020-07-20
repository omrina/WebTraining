using System;
using MongoDB.Bson;

namespace Server.Models
{
    public abstract class BaseContentItem : BaseModel
    {
        public string Content { get; set; }
        public ObjectId AuthorId { get; set; }
        public DateTime Date { get; set; }

        protected BaseContentItem(string content, ObjectId authorId, DateTime date)
        {
            Content = content;
            AuthorId = authorId;
            Date = date;
        }
    }
}