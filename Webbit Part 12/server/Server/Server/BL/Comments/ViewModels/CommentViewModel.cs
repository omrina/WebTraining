using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Server.BL.Ratings.ViewModels;
using Server.Models;

namespace Server.BL.Comments.ViewModels
{
    public class CommentViewModel : BaseVotableViewModel
    {
        public ObjectId Id { get; set; }
        public string SubwebbitId { get; set; }
        public string ThreadId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<CommentViewModel> SubComments { get; set; }

        public CommentViewModel(Comment comment, string subwebbitId, string threadId, ObjectId userId) 
            : base(comment, userId)
        {
            Id = comment.Id;
            SubwebbitId = subwebbitId;
            ThreadId = threadId;
            Content = comment.Content;
            Author = comment.Author;
            Date = comment.Date;
            SubComments = comment.Comments.Select(x => new CommentViewModel(x, subwebbitId, threadId, userId));
        }
    }
}