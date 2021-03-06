﻿using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Server.Models
{
    public class Thread : BaseModel, IVotable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<ObjectId> Upvoters { get; set; }
        public IEnumerable<ObjectId> Downvoters { get; set; }
        public int Rating { get; set; }

        public Thread(string title, string content, string author)
        {
            Id = ObjectId.GenerateNewId();
            Title = title;
            Content = content;
            Author = author;
            Date = DateTime.Now;
            Comments = new List<Comment>();
            Upvoters = new List<ObjectId>();
            Downvoters = new List<ObjectId>();
        }
    }
}