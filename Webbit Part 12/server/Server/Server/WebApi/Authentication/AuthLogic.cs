using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Exceptions;
using Server.Models;
using Server.MongoDB.Extensions;
using Server.WebApi.Authentication.ViewModels;

namespace Server.WebApi.Authentication
{    
    public class AuthLogic : BaseLogic<OnlineUser>
    {
        public async Task<ObjectId> GetUserId(ObjectId token)
        {
            var onlineUser = await GetCollection().AsQueryable().
                                        FirstOrDefaultAsync(x => x.Token == token);

            if (onlineUser == null)
            {
                throw new UnauthenticatedRequestException();
            }

            return onlineUser.Id;
        }

        public async Task<UserViewModel> Login(UserAuthViewModel loginViewModel)
        {
            // TODO: add hashes to passwords
            var user =  await Database.GetCollection<User>().AsQueryable()
                .SingleOrDefaultAsync(x => x.Password == loginViewModel.Password &&
                                           x.Username == loginViewModel.Username);
            
            if (user == null)
            {
                throw new LoginFailedException();
            }

            var onlineUser = new OnlineUser(user.Id);
            await GetCollection().InsertOneAsync(onlineUser);

            return new UserViewModel(onlineUser.Token, user.Username);
        }

        public async Task Logout()
        {
            await GetCollection().DeleteOneAsync(x => x.Id == UserSession.UserId);
        }
    }
}