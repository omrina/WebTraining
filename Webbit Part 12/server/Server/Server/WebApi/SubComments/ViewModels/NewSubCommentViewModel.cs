using Server.WebApi.Comments.ViewModels;

namespace Server.WebApi.SubComments.ViewModels
{
    public class NewSubCommentViewModel : NewCommentViewModel
    {
        public string ParentCommentId { get; set; }
    }
}