using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Models;
using Server.ViewModels;

namespace Server.Logic
{
    public class ThreadLogic : BaseLogic<Subwebbit>
    {
        private const int ThreadsPerPage = 4;

        public FilterDefinition<Subwebbit> GetThreadFilterDefinition(string subwebbitId, string threadId)
        {
            return FilterBuilder.And(
                FilterBuilder.Where(GenerateByIdFilter<Subwebbit>(subwebbitId)),
                FilterBuilder.ElemMatch(x => x.Threads,
                    GenerateByIdFilter<Thread>(threadId)));
        }

        public async Task<ThreadViewModel> GetViewModel(string subwebbitId, string threadId)
        {
            var subwebbit = await new SubwebbitLogic {UserId = UserId}.GetViewModel(subwebbitId);
            var thread = await Get(subwebbitId).SelectMany(x => x.Threads)
                            .FirstAsync(GenerateByIdFilter<Thread>(threadId));

            return new ThreadViewModel(thread, subwebbit, UserId);
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
                threads.Add(await GetViewModel(subwebbitId, threadId.ToString()));
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
                var subwebbitId = await subwebbits.Where(x => x.Threads.Any(thread => thread.Id == threadId))
                                    .Select(x => x.Id).FirstAsync();

                threads.Add(await GetViewModel(subwebbitId.ToString(), threadId.ToString()));
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

        public async Task Delete(string subwebbitId, string threadId)
        {
            await new SubwebbitLogic().EnsureOwnership(subwebbitId, UserId);

            var threadToDelete = await Get(subwebbitId).SelectMany(x => x.Threads)
                .FirstAsync(GenerateByIdFilter<Thread>(threadId));

            await Collection.UpdateOneAsync(GenerateByIdFilter<Subwebbit>(subwebbitId), 
                UpdateBuilder.Pull(x => x.Threads, threadToDelete));
        }
    }
}