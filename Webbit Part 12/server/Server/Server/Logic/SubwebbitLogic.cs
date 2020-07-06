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
                // LOG HERE
                throw new SubwebbitNameAlreadyTakenException();
            }

            return newSubwebbit.Id;
        }

        public new async Task<Subwebbit> Get(string id)
        {
            var subwebbit = await base.Get(id).SingleOrDefaultAsync();

            if (subwebbit == null)
            {
                throw new SubwebbitNotFoundException();
            }

            return subwebbit;
        }

        public async Task<IEnumerable<ThreadViewModel>> GetThreads(FetchThreadsViewModel fetchThreads)
        {
            var threads = await base.Get(fetchThreads.SubwebbitId).SelectMany(x => x.Threads)
                .OrderByDescending(x => x.Date).Skip(fetchThreads.Index).Take(fetchThreads.Amount).ToListAsync();

            return threads.Select(x => new ThreadViewModel(x));
        }

        public async Task CreateThread(NewThreadViewModel thread)
        {
            // TODO: validate thread details!!!
            var newThread = new Thread(thread.Title, thread.Content, thread.Author);
            await Collection.UpdateOneAsync(GenerateByIdFilter(thread.SubwebbitId),
                Builders<Subwebbit>.Update.AddToSet(x => x.Threads, newThread));
        }
    }
}