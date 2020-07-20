using System;
using System.Linq;
using MongoDB.Bson;
using Server.Models;
using Server.WebApi.Ratings.ViewModels;
using Server.WebApi.Subwebbits.ViewModels;

namespace Server.WebApi.Threads.ViewModels
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
            // TODO: fix author! (get it as ctor param?)
            Author = "sdf";
            // Author = thread.Author;
            Date = thread.Date;
            CommentsCount = thread.Comments.Sum(x => x.Comments.Count() + 1);
            IsOwner = subwebbit.IsOwner;
        }
    }
}