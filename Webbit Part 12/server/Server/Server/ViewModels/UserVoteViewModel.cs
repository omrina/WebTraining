using Server.Enums;

namespace Server.ViewModels
{
    public class UserVoteViewModel
    {
        public string SubwebbitId { get; set; }
        public string ThreadId { get; set; }
        public string CommentId { get; set; }
        public string ParentCommentId { get; set; }
        public VoteDirections NewDirection { get; set; }
        public VoteDirections PreviousDirection { get; set; }
    }
}