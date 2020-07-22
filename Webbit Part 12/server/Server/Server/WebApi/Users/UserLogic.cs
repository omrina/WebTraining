using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Exceptions;
using Server.Models;
using Server.MongoDB.Extensions;
using Server.WebApi.Authentication;
using Server.WebApi.Authentication.Validation;
using Server.WebApi.Authentication.ViewModels;
using Server.WebApi.Subwebbits;

namespace Server.WebApi.Users
{
    public class UserLogic : BaseLogic<User>
    {
        private const int SubscriptionsOnSignup = 10;

        public async Task Signup(UserAuthViewModel user)
        {
            await EnsureSignupDetails(user);
            var newUser = new User(user.Username, user.Password)
            {
                SubscribedSubwebbits = await GetMostSubscribed(SubscriptionsOnSignup)
            };

            await GetCollection().InsertOneAsync(newUser);

            foreach (var subwebbitId in newUser.SubscribedSubwebbits)
            {
                await new SubwebbitLogic().IncrementSubscribers(subwebbitId);
            }
        }

        private async Task EnsureSignupDetails(UserAuthViewModel user)
        {
            if (!new SignupDetailsValidator().IsValid(user))
            {
                throw new InvalidModelDetailsException();
            }

            await EnsureUsernameNotTaken(user.Username);
        }

        private async Task EnsureUsernameNotTaken(string username)
        {
            if (await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(x => x.Username == username) != null)
            {
                throw new UsernameAlreadyTakenException();
            }
        }

        private async Task<IEnumerable<ObjectId>> GetMostSubscribed(int amount)
        {
            return await Database.GetCollection<Subwebbit>().AsQueryable()
                        .OrderByDescending(x => x.SubscribersCount)
                        .Take(amount).Select(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<ObjectId>> GetSubscribedIds()
        {
            return (await Get(UserSession.UserId)).SubscribedSubwebbits;
        }

        public async Task<string> GetName(ObjectId id)
        {
            return (await Get(id)).Username;
        }
    }
}