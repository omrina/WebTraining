using MongoDB.Bson;

namespace Server.WebApi.Authentication.ViewModels
{
    public class UserViewModel
    {
        public ObjectId Token { get; set; }
        public string Username { get; set; }

        public UserViewModel(ObjectId token, string username)
        {
            Token = token;
            Username = username;
        }
    }
}