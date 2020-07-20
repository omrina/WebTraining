using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Exceptions;
using Server.Models;
using Server.WebApi.Authentication;
using Server.WebApi.Subwebbits;
using Server.WebApi.Threads.Validation;
using Server.WebApi.Threads.ViewModels;

namespace Server.WebApi.Threads
{
    public class ThreadLogic : BaseLogic<Thread>
    {
        private const int ThreadsPerPage = 4;

        public async Task<ThreadViewModel> GetViewModel(string subwebbitId, string threadId)
        {
            var subwebbit = await new SubwebbitLogic().GetViewModel(subwebbitId);
            var thread = await GetCollection().AsQueryable()
                            .FirstAsync(x => x.Id == ObjectId.Parse(threadId));

            return new ThreadViewModel(thread, subwebbit, UserSession.UserId);
        }

        public async Task<IEnumerable<ThreadViewModel>> GetRecentThreads(string subwebbitId, int index)
        {
            // TODO: make this func work!
            var subwebbit = new Subwebbit("sdf","ds");
            // var subwebbit = Database.GetCollection<Subwebbit>().;
            var threadsIds = subwebbit.Threads
                // .OrderByDescending(x => x.Date).Skip(index).Take(ThreadsPerPage).Select(x => x.Token)
                ;

            var threads = new List<ThreadViewModel>();

            foreach (var threadId in threadsIds)
            {
                threads.Add(await GetViewModel(subwebbitId, threadId.ToString()));
            }

            return threads;
        }

        public async Task<IEnumerable<ThreadViewModel>> GetTopThreadsFromSubscribed(int index)
        {
            // TODO: make this func work!
            // var subwebbitsIds = await new UserLogic().GetSubscribedIds(Token.ToString());
            var subwebbitsIds = new List<ObjectId>();
            var subwebbits = GetCollection().AsQueryable().Where(x => subwebbitsIds.Contains(x.Id));

            var threadsIds = new List<ObjectId>();
            // var threadsIds = await subwebbits.SelectMany(x => x.Threads)
                // .OrderByDescending(x => x.Rating).Skip(index).Take(ThreadsPerPage).Select(x => x.Token)
                // .ToListAsync();

            var threads = new List<ThreadViewModel>();

            foreach (var threadId in threadsIds)
            {
                // var subwebbitId = await subwebbits.Where(x => x.Threads.Any(thread => thread.Token == threadId))
                                    // .Select(x => x.Token).FirstAsync();

                threads.Add(await GetViewModel("Sf", threadId.ToString()));
                // threads.Add(await GetViewModel(subwebbitId.ToString(), threadId.ToString()));
            }

            return threads;
        }

        public async Task Create(NewThreadViewModel thread)
        {
            EnsureThreadDetails(thread);
            var newThread = new Thread(thread.Content, UserSession.UserId, DateTime.Now, thread.Title);

            await GetCollection().InsertOneAsync(newThread);
            // TODO: add also to subwebbit threads list!
        }

        private void EnsureThreadDetails(NewThreadViewModel thread)
        {
            if (!new ThreadValidator().IsValid(thread))
            {
                throw new InvalidModelDetailsException();
            }
        }

        public async Task Delete(ObjectId subwebbitId, string threadId)
        {
            await new SubwebbitLogic().EnsureOwnership(subwebbitId, UserSession.UserId);

            // TODO: add extension of delete by id?
            await GetCollection().DeleteOneAsync(x => x.Id == ObjectId.Parse(threadId));
            // TODO: delete also from subwebbit threads list!
        }
    }
}