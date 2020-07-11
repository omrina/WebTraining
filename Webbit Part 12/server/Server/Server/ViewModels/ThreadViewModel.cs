using System;
using System.Linq;
using MongoDB.Bson;
using Server.Models;

namespace Server.ViewModels
{
    public class ThreadViewModel
    {
        public ObjectId Id { get; set; }
        public ObjectId SubwebbitId { get; set; }
        public string SubwebbitName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public int CommentsCount { get; set; }
        // TODO: add my rating (1 / 0 / -1 ?)

        public ThreadViewModel(Thread thread, ObjectId subwebbitId, string subwebbitName)
        {
            Id = thread.Id;
            SubwebbitId = subwebbitId;
            SubwebbitName = subwebbitName;
            Title = thread.Title;
            Content = thread.Content;
            Author = thread.Author;
            Date = thread.Date;
            Rating = thread.Rating;
            CommentsCount = thread.Comments.Sum(x => x.Comments.Count() + 1);
        }
    }
}
