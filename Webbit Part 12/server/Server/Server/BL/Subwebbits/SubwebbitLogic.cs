using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.BL.Subwebbits.Validation;
using Server.BL.Subwebbits.ViewModels;
using Server.BL.Users;
using Server.Exceptions;
using Server.Models;

namespace Server.BL.Subwebbits
{
    public class SubwebbitLogic : BaseLogic<Subwebbit>
    {
        public async Task<bool> IsSubwebbitNameExists(string name)
        {
            return await GetAll().Where(x => x.Name == name)
                .Select(x => x.Name).FirstOrDefaultAsync() != null;
        }

        public async Task<IEnumerable<SearchedSubwebbitViewModel>> GetAllByName(string name)
        {
            var matchingSubwebbits = await GetAll().Where(x => x.Name.Contains(name)).ToListAsync();

            return matchingSubwebbits.Select(x => new SearchedSubwebbitViewModel(x));
        }

        public async Task<ObjectId> Create(NewSubwebbitViewModel subwebbit)
        {
            EnsureSubwebbitDetails(subwebbit);
            await EnsureNameNotTaken(subwebbit.Name);

            var newSubwebbit = new Subwebbit(subwebbit.OwnerId, subwebbit.Name);
            await Collection.InsertOneAsync(newSubwebbit);

            return newSubwebbit.Id;
        }

        private void EnsureSubwebbitDetails(NewSubwebbitViewModel subwebbit)
        {
            if (!new SubwebbitValidator().IsValid(subwebbit))
            {
                throw new InvalidModelDetailsException();
            }
        }

        private async Task EnsureNameNotTaken(string name)
        {
            if (await IsSubwebbitNameExists(name))
            {
                throw new SubwebbitNameAlreadyTakenException();
            }
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
            await EnsureOwnership(id, UserId);
            await Collection.DeleteOneAsync(GenerateByIdFilter<Subwebbit>(id));
        }

        public async Task EnsureOwnership(string subwebbitId, ObjectId userId)
        {
            var subwebbitOwnerId = await Get(subwebbitId).Select(x => x.OwnerId).FirstAsync();

            if (subwebbitOwnerId != userId)
            {
                throw new UserNotOwnerException();
            }
        }
    }
}