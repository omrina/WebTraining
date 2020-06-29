using Server.Models;

namespace Server.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public UserViewModel(User user)
        {
            Id = user.Id.ToString();
            Username = user.Username;
        }
    }
}