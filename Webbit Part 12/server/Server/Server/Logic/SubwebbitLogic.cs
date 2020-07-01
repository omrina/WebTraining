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
        public IEnumerable<SearchedSubwebbitViewModel> GetAllByNameMatch(string name)
        {
            var matchingSubwebbits = GetAll().Where(x => x.Name.Contains(name)).ToList();

            return matchingSubwebbits.Select(x => new SearchedSubwebbitViewModel(x));
        }

        public async Task<ObjectId> Create(NewSubwebbitViewModel subwebbit)
        {
            var newSubwebbit = new Subwebbit(new ObjectId(subwebbit.OwnerId), subwebbit.Name);
            // TODO: check name already exists exception
            try
            {
                await Collection.InsertOneAsync(newSubwebbit);
            }
            catch (MongoWriteException e) when (e.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                // TODO: Logger!?
                throw new SubwebbitNameAlreadyTakenException();
            }

            return newSubwebbit.Id;
        }

        public async Task<SubwebbitViewModel> Get(string id)
        {
            var subwebbit = await GetAll().SingleOrDefaultAsync(x => x.Id == new ObjectId(id));

            if (subwebbit == null)
            {
                throw new SubwebbitNotFoundException();   
            }

            return new SubwebbitViewModel(subwebbit);
        }
    }
}