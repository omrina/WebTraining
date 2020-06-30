using Server.Models;

namespace Server.ViewModels
{
    public class SearchedSubwebbitViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long SubscribersCount { get; set; }

        public SearchedSubwebbitViewModel(Subwebbit subwebbit)
        {
            Id = subwebbit.Id.ToString();
            Name = subwebbit.Name;
            SubscribersCount = subwebbit.SubscribersCount;
        }
    }
}