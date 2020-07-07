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
        private const int ThreadsPerPage = 3;

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

        public new async Task<SubwebbitViewModel> Get(string id)
        {
            var subwebbit = await base.Get(id).SingleOrDefaultAsync();

            if (subwebbit == null)
            {
                throw new SubwebbitNotFoundException();
            }

            return new SubwebbitViewModel(subwebbit);
        }

        public async Task<IEnumerable<ThreadViewModel>> GetThreads(string subwebbitId, int index)
        {
            var subwebbitName = (await Get(subwebbitId)).Name;
            var threads = await base.Get(subwebbitId).SelectMany(x => x.Threads)
                .OrderByDescending(x => x.Date).Skip(index).Take(ThreadsPerPage)
                .ToListAsync();

            return threads.Select(x => new ThreadViewModel(x, subwebbitName));
        }

        public async Task<IEnumerable<ThreadViewModel>> GetTopThreadsFromSubscribed
            (string userId, int index)
        {
            var subwebbitsIds = (await new UserLogic().GetSubscribedIds(userId))
                                .ToList();
            var subwebbits = GetMany(subwebbitsIds);

            var threads = await subwebbits.SelectMany(x => x.Threads).OrderByDescending(x => x.Rating)
                            .Skip(index).Take(ThreadsPerPage).ToListAsync();

            return threads.Select(thread =>
                new ThreadViewModel(thread, subwebbits.First(subwebbit =>
                                                             subwebbit.Threads.Contains(thread)).Name));
        }

        private IMongoQueryable<Subwebbit> GetMany(IEnumerable<ObjectId> ids)
        {
            return GetAll().Where(x => ids.Contains(x.Id));
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