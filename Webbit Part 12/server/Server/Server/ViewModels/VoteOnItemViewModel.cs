using Server.Enums;

namespace Server.ViewModels
{
    public class VoteOnItemViewModel
    {
        // TODO: change to id's hierarchy   
        public string ItemId { get; set; }
        public VoteDirections Direction { get; set; }
    }
}