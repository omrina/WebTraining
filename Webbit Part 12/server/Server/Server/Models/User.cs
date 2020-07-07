using System.Collections.Generic;
using MongoDB.Bson;

namespace Server.Models
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string SessionToken { get; set; }
        public IEnumerable<ObjectId> SubscribedSubwebbits { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}