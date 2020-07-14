namespace Server.BL.Ratings.ViewModels
{
    public class UserVoteViewModel
    {
        public string SubwebbitId { get; set; }
        public string ThreadId { get; set; }
        public string CommentId { get; set; }
        public string ParentCommentId { get; set; }
        public VoteChangeViewModel VoteChange { get; set; }
    }
}