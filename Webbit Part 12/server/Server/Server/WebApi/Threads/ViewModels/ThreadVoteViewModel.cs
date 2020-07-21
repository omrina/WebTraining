using Server.WebApi.Ratings.Enums;
using Server.WebApi.Ratings.ViewModels;

namespace Server.WebApi.Threads.ViewModels
{
    public class ThreadVoteViewModel
    {
        public string Id { get; set; }
        public VoteStates Vote { get; set; }
    }
}