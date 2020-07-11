using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Server.Models;

namespace Server.ViewModels
{
    public class CommentViewModel
    {
        public ObjectId Id { get; set; }
        public string SubwebbitId { get; set; }
        public string ThreadId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public IEnumerable<Comment> SubComments { get; set; }
        // TODO: add my rating (1 / 0 / -1 ?) 
        // TODO: IVotableViewModel????

        public CommentViewModel(Comment comment, string subwebbitId, string threadId)
        {
            Id = comment.Id;
            SubwebbitId = subwebbitId;
            ThreadId = threadId;
            Content = comment.Content;
            Author = comment.Author;
            Date = comment.Date;
            Rating = comment.Rating; // TODO: upvoters - downvoters
            SubComments = comment.Comments;
        }
    }
}