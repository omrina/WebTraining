using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Exceptions;
using Server.Models;
using Server.WebApi.Authentication;
using Server.WebApi.Subwebbits.Validation;
using Server.WebApi.Subwebbits.ViewModels;

namespace Server.WebApi.Subwebbits
{
    public class SubwebbitLogic : BaseLogic<Subwebbit>
    {
        

        public async Task<IEnumerable<SearchedSubwebbitViewModel>> GetAllByName(string name)
        {
            var matchingSubwebbits = await GetCollection().AsQueryable().Where(x => x.Name.Contains(name)).ToListAsync();

            return matchingSubwebbits.Select(x => new SearchedSubwebbitViewModel(x));
        }

        public async Task<ObjectId> Create(NewSubwebbitViewModel subwebbit)
        {
            await EnsureSubwebbitDetails(subwebbit);
            var newSubwebbit = new Subwebbit(subwebbit.OwnerId, subwebbit.Name);

            await GetCollection().InsertOneAsync(newSubwebbit);

            return newSubwebbit.Id;
        }

        private async Task EnsureSubwebbitDetails(NewSubwebbitViewModel subwebbit)
        {
            if (!new SubwebbitValidator().IsValid(subwebbit))
            {
                throw new InvalidModelDetailsException();
            }

            await EnsureNameNotTaken(subwebbit.Name);
        }

        private async Task EnsureNameNotTaken(string name)
        {
            if (await IsSubwebbitNameExists(name))
            {
                throw new SubwebbitNameAlreadyTakenException();
            }
        }

        private async Task<bool> IsSubwebbitNameExists(string name)
        {
            return await GetCollection().AsQueryable().Where(x => x.Name == name)
                .Select(x => x.Name).FirstOrDefaultAsync() != null;
        }

        public async Task<SubwebbitViewModel> GetViewModel(string subwebbitId)
        {
            var subwebbit = await Get(ObjectId.Parse(subwebbitId));

            var isSubscribed = await IsSubscribed(UserSession.UserId, subwebbitId);

            return new SubwebbitViewModel(subwebbit, isSubscribed, UserSession.UserId);
        }

        public async Task Delete(ObjectId id)
        {
            await EnsureOwnership(id, UserSession.UserId);
            await GetCollection().DeleteOneAsync(x => x.Id == id);
        }

        public async Task EnsureOwnership(ObjectId subwebbitId, ObjectId userId)
        {
            var subwebbitOwnerId = (await Get(subwebbitId)).OwnerId;

            if (subwebbitOwnerId != userId)
            {
                throw new UserNotOwnerException();
            }
        }

        public async Task<IEnumerable<ObjectId>> GetSubscribedIds(ObjectId userId)
        {
            return await Database.GetCollection<User>(nameof(User)).AsQueryable()
                .SelectMany(x => x.SubscribedSubwebbits).ToListAsync();
        }

        public async Task<bool> IsSubscribed(ObjectId userId, string subwebbitId)
        {
            return (await GetSubscribedIds(userId)).Contains(ObjectId.Parse(subwebbitId));
        }

        public async Task Subscribe(string subwebbitId)
        {
            var usersCollection = Database.GetCollection<User>(nameof(User));

            await usersCollection.UpdateOneAsync(x => x.Id == UserSession.UserId,
                Builders<User>.Update.AddToSet(x => x.SubscribedSubwebbits, ObjectId.Parse(subwebbitId)));
            await new SubwebbitSubscriptionLogic().IncrementSubscribers(subwebbitId);
        }

        public async Task Unsubscribe(string subwebbitId)
        {
            var usersCollection = Database.GetCollection<User>(nameof(User));

            await usersCollection.UpdateOneAsync(x => x.Id == UserSession.UserId,
                Builders<User>.Update.Pull(x => x.SubscribedSubwebbits, ObjectId.Parse(subwebbitId)));
            await new SubwebbitSubscriptionLogic().DecrementSubscribers(subwebbitId);
        }
    }
}