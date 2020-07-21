using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Server.Models;
using Server.WebApi.Ratings.ViewModels;

namespace Server.WebApi.Comments.ViewModels
{
    public class CommentViewModel : BaseVotableViewModel
    {
        public ObjectId Id { get; set; }
        public ObjectId ThreadId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<CommentViewModel> SubComments { get; set; }

        public CommentViewModel(Comment comment, ObjectId threadId, ObjectId userId, string author,
                                IEnumerable<CommentViewModel> subComments) 
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