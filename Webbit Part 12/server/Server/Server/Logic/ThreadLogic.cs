using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Models;
using Server.ViewModels;

namespace Server.Logic
{
    public class ThreadLogic : BaseLogic<Subwebbit>
    {
        private const int ThreadsPerPage = 4;
        // TODO: make user id prop in base logic so controllers would set it
        // maybe on ctor of base controller put this in its base logic!

        public FilterDefinition<Subwebbit> GetThreadFilterDefinition(string subwebbitId, string threadId)
        {
            return FilterBuilder.And(
                FilterBuilder.Where(GenerateByIdFilter<Subwebbit>(subwebbitId)),
                FilterBuilder.ElemMatch(x => x.Threads,
                    GenerateByIdFilter<Thread>(threadId)));
        }

        public async Task<ThreadViewModel> Get(string subwebbitId, string threadId)
        {
            var subwebbit = Get(subwebbitId);

            return await GetThreadViewModel(new ObjectId(threadId), subwebbit);
        }

        private async Task<ThreadViewModel> GetThreadViewModel(ObjectId threadId, IMongoQueryable<Subwebbit> subwebbit)
        {
            var subwebbitName = await subwebbit.Select(x => x.Name).FirstAsync();
            var subwebbitId = await subwebbit.Select(x => x.Id).FirstAsync();
            var thread = await subwebbit.SelectMany(x => x.Threads).FirstAsync(x => x.Id == threadId);

            return new ThreadViewModel(thread, subwebbitId, subwebbitName, UserId);
        }

        public async Task<IEnumerable<ThreadViewModel>> GetRecentThreads(string subwebbitId, int index)
        {
            var subwebbit = Get(subwebbitId);
            var threadsIds = await subwebbit.SelectMany(x => x.Threads)
                .OrderByDescending(x => x.Date).Skip(index).Take(ThreadsPerPage).Select(x => x.Id)
                .ToListAsync();

            var threads = new List<ThreadViewModel>();

            foreach (var threadId in threadsIds)
            {
                threads.Add(await GetThreadViewModel(threadId, subwebbit));
            }

            return threads;
        }

        public async Task<IEnumerable<ThreadViewModel>> GetTopThreadsFromSubscribed(int index)
        {
            var subwebbitsIds = await new UserLogic().GetSubscribedIds(UserId.ToString());
            var subwebbits = GetAll().Where(x => subwebbitsIds.Contains(x.Id));

            var threadsIds = await subwebbits.SelectMany(x => x.Threads)
                .OrderByDescending(x => x.Rating).Skip(index).Take(ThreadsPerPage).Select(x => x.Id)
                .ToListAsync();

            var threads = new List<ThreadViewModel>();

            foreach (var threadId in threadsIds)
            {
                var subwebbit = subwebbits.Where(x => x.Threads.Any(thread => thread.Id == threadId));

                threads.Add(await GetThreadViewModel(threadId, subwebbit));
            }

            return threads;
        }

        public async Task Create(NewThreadViewModel thread)
        {
            // TODO: validate thread details!!!
            var newThread = new Thread(thread.Title, thread.Content, thread.Author);
            await Collection.UpdateOneAsync(GenerateByIdFilter<Subwebbit>(thread.SubwebbitId),
                UpdateBuilder.AddToSet(x => x.Threads, newThread));
        }
    }
}