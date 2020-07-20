using MongoDB.Bson;
using Server.Models;

namespace Server.WebApi.Subwebbits.ViewModels
{
    public class SearchedSubwebbitViewModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public long SubscribersCount { get; set; }

        public SearchedSubwebbitViewModel(Subwebbit subwebbit)
        {
            Id = subwebbit.Id;
            Name = subwebbit.Name;
            SubscribersCount = subwebbit.SubscribersCount;
        }
    }
}