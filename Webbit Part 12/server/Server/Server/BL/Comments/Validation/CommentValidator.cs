using Server.BL.Comments.ViewModels;

namespace Server.BL.Comments.Validation
{
    public class CommentValidator
    {
        public bool IsValid(NewCommentViewModel comment)
        {
            return !string.IsNullOrWhiteSpace(comment.SubwebbitId) &&
                   !string.IsNullOrWhiteSpace(comment.ThreadId) &&
                   !string.IsNullOrWhiteSpace(comment.Content) &&
                   !string.IsNullOrWhiteSpace(comment.Author);
        }
    }
}