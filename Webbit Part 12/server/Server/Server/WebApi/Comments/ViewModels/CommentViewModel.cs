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
        public string ThreadId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<CommentViewModel> SubComments { get; set; }

        public CommentViewModel(Comment comment, string threadId, ObjectId userId) 
            : base(comment, userId)
        {
            Id = comment.Id;
            ThreadId = threadId;
            Content = comment.Content;
            // TODO: fix author! (get it as ctor param?)
            Author = "sdf";
            // Author = (new UserLogic().Get(comment.AuthorId)).;
            Date = comment.Date;
            SubComments = comment.Comments.Select(x => new CommentViewModel(x, threadId, userId));
        }
    }
}