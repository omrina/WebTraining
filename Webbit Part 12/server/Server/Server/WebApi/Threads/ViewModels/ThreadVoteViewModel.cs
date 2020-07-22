using Server.WebApi.RatingSystem.Enums;

namespace Server.WebApi.Threads.ViewModels
{
    public class ThreadVoteViewModel
    {
        public string Id { get; set; }
        public VoteStates Vote { get; set; }
    }
}