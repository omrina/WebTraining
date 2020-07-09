using Server.Enums;

namespace Server.ViewModels
{
    public class VoteOnItemViewModel
    {
        public string ItemId { get; set; }
        public VoteDirections Direction { get; set; }
    }
}