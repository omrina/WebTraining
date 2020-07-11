using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Exceptions;
using Server.Models;
using Server.ViewModels;

namespace Server.Logic
{
    public class SubwebbitLogic : BaseLogic<Subwebbit>
    {
        public async Task<IEnumerable<SearchedSubwebbitViewModel>> GetAllByName(string name)
        {
            var matchingSubwebbits = await GetAll().Where(x => x.Name.Contains(name)).ToListAsync();

            return matchingSubwebbits.Select(x => new SearchedSubwebbitViewModel(x));
        }

        public async Task<ObjectId> Create(NewSubwebbitViewModel subwebbit)
        {
            var newSubwebbit = new Subwebbit(subwebbit.OwnerId, subwebbit.Name);
            // TODO: change validation of subwebbit name existence (and change the script for subwebbit's name index)
            // TODO: check name already exists exception
            try
            {
                await Collection.InsertOneAsync(newSubwebbit);
            }
            catch (MongoWriteException e) when (e.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                // LOG HERE
                throw new SubwebbitNameAlreadyTakenException();
            }

            return newSubwebbit.Id;
        }

        public async Task<SubwebbitViewModel> Get(string subwebbitId, string userId)
        {
            var subwebbit = await Get(subwebbitId).SingleOrDefaultAsync();

            if (subwebbit == null)
            {
                throw new SubwebbitNotFoundException();
            }

            var isSubscribed = await new UserLogic().IsSubscribed(userId, subwebbitId);

            return new SubwebbitViewModel(subwebbit, isSubscribed);
        }

        public async Task IncrementSubscribers(string subwebbitId)
        {
            await AddToSubscribersCount(subwebbitId, 1);
        }

        public async Task DecrementSubscribers(string subwebbitId)
        {
            await AddToSubscribersCount(subwebbitId, -1);
        }

        private async Task AddToSubscribersCount(string subwebbitId, int value)
        {
            await Collection.UpdateOneAsync(GenerateByIdFilter(subwebbitId),
                Builders<Subwebbit>.Update.Inc(x => x.SubscribersCount, value));
        }
    }
}