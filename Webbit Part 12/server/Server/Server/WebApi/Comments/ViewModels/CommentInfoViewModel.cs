using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Server.Models;
using Server.WebApi.RatingSystem.ViewModels;

namespace Server.WebApi.Comments.ViewModels
{
    public class CommentInfoViewModel : VoteInfoViewModel
    {
        public ObjectId Id { get; set; }
        public ObjectId ThreadId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<CommentInfoViewModel> SubComments { get; set; }

        public CommentInfoViewModel(Comment comment, ObjectId threadId, ObjectId userId, string author,
                                IEnumerable<CommentInfoViewModel> subComments) 
            : base(comment, userId)
        {
            Id = comment.Id;
            ThreadId = threadId;
            Content = comment.Content;
            Author = author;
            Date = comment.Date;
            SubComments = subComments;
        }
    }
}