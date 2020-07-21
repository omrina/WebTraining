using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Exceptions;
using Server.Models;
using Server.MongoDB.Extensions;
using Server.WebApi.Authentication;
using Server.WebApi.Subwebbits.Validation;
using Server.WebApi.Subwebbits.ViewModels;
using Server.WebApi.Users;

namespace Server.WebApi.Subwebbits
{
    public class SubwebbitLogic : BaseLogic<Subwebbit>
    {
        public async Task<IEnumerable<SearchedSubwebbitViewModel>> Search(string name)
        {
            var subwebbits = await GetCollection().AsQueryable()
                .Where(x => x.Name.Contains(name)).ToListAsync();

            return subwebbits.Select(x => new SearchedSubwebbitViewModel(x));
        }

        public async Task<SubwebbitViewModel> Create(string name)
        {
            await EnsureSubwebbitDetails(name);
            var newSubwebbit = new Subwebbit(UserSession.UserId, name);

            await Create(newSubwebbit);

            return await GetById(newSubwebbit.Id);
        }

        private async Task EnsureSubwebbitDetails(string name)
        {
            if (!new SubwebbitValidator().IsValid(name))
            {
                throw new InvalidModelDetailsException();
            }

            await EnsureNameNotTaken(name);
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

        public async Task<SubwebbitViewModel> GetById(ObjectId id)
        {
            var subwebbit = await Get(id);

            return new SubwebbitViewModel(subwebbit,
                                          await IsSubscribed(id),
                                          subwebbit.OwnerId == UserSession.UserId);
        }

        private async Task<bool> IsSubscribed(ObjectId id)
        {
            // TODO: should be using the subscribersIds list of the subwebbit
            // (instead of the subs count)
            var subscribedIds = await new UserLogic().GetSubscribedIds();

            return subscribedIds.Contains(id);
        }

        public async Task Delete(ObjectId id)
        {
            await EnsureOwnership(id, UserSession.UserId);
            var threadsIds = (await Get(id)).Threads;

            await GetCollection().DeleteOneAsync(x => x.Id == id);
            await Database.GetCollection<Thread>().DeleteManyAsync(x => threadsIds.Contains(x.Id));
        }

        public async Task EnsureOwnership(ObjectId id, ObjectId userId)
        {
            var subwebbitOwnerId = (await Get(id)).OwnerId;

            if (subwebbitOwnerId != userId)
            {
                throw new UserNotOwnerException();
            }
        }

        public async Task Subscribe(ObjectId id)
        {
            var usersCollection = Database.GetCollection<User>();

            await usersCollection.UpdateOneAsync(x => x.Id == UserSession.UserId,
                Builders<User>.Update.AddToSet(x => x.SubscribedSubwebbits, id));
            await new SubwebbitSubscriptionLogic().IncrementSubscribers(id);
        }

        public async Task Unsubscribe(ObjectId id)
        {
            var usersCollection = Database.GetCollection<User>();

            await usersCollection.UpdateOneAsync(x => x.Id == UserSession.UserId,
                Builders<User>.Update.Pull(x => x.SubscribedSubwebbits, id));
            await new SubwebbitSubscriptionLogic().DecrementSubscribers(id);
        }
    }
}