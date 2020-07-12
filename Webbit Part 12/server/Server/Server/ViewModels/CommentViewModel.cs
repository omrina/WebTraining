using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Server.Enums;
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
        public IEnumerable<CommentViewModel> SubComments { get; set; }
        public int Rating { get; set; }
        public VoteDirections UserVote { get; set; }
        // TODO: IVotableViewModel????

        public CommentViewModel(Comment comment, string subwebbitId, string threadId, ObjectId userId)
        {
            Id = comment.Id;
            SubwebbitId = subwebbitId;
            ThreadId = threadId;
            UserVote = GetUserVote(comment, userId);
            Content = comment.Content;
            Author = comment.Author;
            Date = comment.Date;
            Rating = comment.Rating;
            SubComments = comment.Comments.Select(x => new CommentViewModel(x, subwebbitId, threadId, userId));
        }

        private VoteDirections GetUserVote(IVotable item, ObjectId userId)
        {
            return item.Upvoters.Contains(userId)
                ? VoteDirections.Up
                : item.Downvoters.Contains(userId) 
                    ? VoteDirections.Down 
                    : VoteDirections.Cancel;
        }
    }
}