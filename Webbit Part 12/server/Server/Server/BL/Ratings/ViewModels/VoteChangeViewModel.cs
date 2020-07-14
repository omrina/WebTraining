using Server.BL.Ratings.Enums;

namespace Server.BL.Ratings.ViewModels
{
    public class VoteChangeViewModel
    {
        public VoteDirections NewDirection { get; set; }
        public VoteDirections PreviousDirection { get; set; }
    }
}