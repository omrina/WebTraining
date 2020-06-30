using Server.Models;

namespace Server.ViewModels
{
    public class SearchedSubwebbitViewModel
    {
        public string Name { get; set; }
        public long SubscribersCount { get; set; }

        public SearchedSubwebbitViewModel(Subwebbit subwebbit)
        {
            Name = subwebbit.Name;
            SubscribersCount = subwebbit.SubscribersCount;
        }
    }
}