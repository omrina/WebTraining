using System;
using System.Linq;
using MongoDB.Bson;
using Server.BL.Ratings.ViewModels;
using Server.BL.Subwebbits.ViewModels;
using Server.Models;

namespace Server.BL.Threads.ViewModels
{
    public class ThreadViewModel : BaseVotableViewModel
    {
        public ObjectId Id { get; set; }
        public ObjectId SubwebbitId { get; set; }
        public string SubwebbitName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public int CommentsCount { get; set; }
        public bool IsOwner { get; set; }

        public ThreadViewModel(Thread thread, SubwebbitViewModel subwebbit, ObjectId userId)
            : base(thread, userId)
        {
            Id = thread.Id;
            SubwebbitId = subwebbit.Id;
            SubwebbitName = subwebbit.Name;
            Title = thread.Title;
            Content = thread.Content;
            Author = thread.Author;
            Date = thread.Date;
            CommentsCount = thread.Comments.Sum(x => x.Comments.Count() + 1);
            IsOwner = subwebbit.IsOwner;
        }
    }
}