using MongoDB.Bson;
using Server.Models;

namespace Server.ViewModels
{
    public class UserViewModel
    {
        public ObjectId Id { get; set; }
        public string Username { get; set; }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Username = user.Username;
        }
    }
}