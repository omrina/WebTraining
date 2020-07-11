namespace Server.ViewModels
{
    public class NewCommentViewModel
    {
        public string SubwebbitId { get; set; }
        public string ThreadId { get; set; }
        public string ParentCommentId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}