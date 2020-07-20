using Server.WebApi.Comments.ViewModels;

namespace Server.WebApi.Comments.Validation
{
    public class CommentValidator
    {
        public bool IsValid(NewCommentViewModel comment)
        {
            return !string.IsNullOrWhiteSpace(comment.ThreadId) &&
                   !string.IsNullOrWhiteSpace(comment.Content) &&
                   !string.IsNullOrWhiteSpace(comment.Author);
        }
    }
}