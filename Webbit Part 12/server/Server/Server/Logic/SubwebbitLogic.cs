using System;
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

        public async Task<SubwebbitViewModel> GetViewModel(string subwebbitId)
        {
            var subwebbit = await Get(subwebbitId).SingleOrDefaultAsync();

            if (subwebbit == null)
            {
                throw new SubwebbitNotFoundException();
            }

            var isSubscribed = await new UserLogic().IsSubscribed(UserId.ToString(), subwebbitId);

            return new SubwebbitViewModel(subwebbit, isSubscribed, UserId.ToString());
        }

        public async Task Delete(string id)
        {
            // TODO: ensure user is owner
            await EnsureOwnership(id, UserId);
            await Collection.DeleteOneAsync(GenerateByIdFilter<Subwebbit>(id));
        }

        public async Task EnsureOwnership(string subwebbitId, ObjectId userId)
        {
            var subwebbitOwnerId = await Get(subwebbitId).Select(x => x.OwnerId).FirstAsync();

            if (subwebbitOwnerId != userId)
            {
                // TODO: change to not-owner exception (unauthorized code)
                throw new NotImplementedException();
            }
        }
    }
}