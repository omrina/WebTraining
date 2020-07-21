using Server.WebApi.Threads.ViewModels;

namespace Server.WebApi.Comments.ViewModels
{
    public class CommentVoteViewModel : ThreadVoteViewModel
    {
        public string CommentId { get; set; }
    }
}