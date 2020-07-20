using Server.WebApi.Ratings.Enums;

namespace Server.WebApi.Ratings.ViewModels
{
    public class VoteChangeViewModel
    {
        public VoteDirections NewDirection { get; set; }
        public VoteDirections PreviousDirection { get; set; }
    }
}