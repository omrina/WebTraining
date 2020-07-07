using System;
using Server.Models;

namespace Server.ViewModels
{
    public class ThreadViewModel
    {
        public string Id { get; set; }
        public string SubwebbitName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public int CommentsCount { get; set; }
        // TODO: add my rating (1 / 0 / -1 ?)

        public ThreadViewModel(Thread thread, string subwebbitName)
        {
            Id = thread.Id.ToString();
            SubwebbitName = subwebbitName;
            Title = thread.Title;
            Content = thread.Content;
            Author = thread.Author;
            Date = thread.Date;
            Rating = thread.Rating; // TODO: upvoters - downvoters
            CommentsCount = 0; // TODO: complete this
        }
    }
}
