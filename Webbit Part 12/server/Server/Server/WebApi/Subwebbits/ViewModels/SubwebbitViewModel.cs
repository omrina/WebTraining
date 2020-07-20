using MongoDB.Bson;
using Server.Models;

namespace Server.WebApi.Subwebbits.ViewModels
{
    public class SubwebbitViewModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public long SubscribersCount { get; set; }
        public bool IsSubscribed { get; set; }
        public bool IsOwner { get; set; }

        public SubwebbitViewModel(Subwebbit subwebbit, bool isSubscribed, ObjectId userId)
        {
            Id = subwebbit.Id;
            Name = subwebbit.Name;
            SubscribersCount = subwebbit.SubscribersCount;
            IsSubscribed = isSubscribed;
            IsOwner = subwebbit.OwnerId == userId;
        }
    }
}