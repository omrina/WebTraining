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
using Server.WebApi.RatingSystem;
using Server.WebApi.RatingSystem.ViewModels;
using Server.WebApi.Subwebbits;
using Server.WebApi.Threads.Validation;
using Server.WebApi.Threads.ViewModels;
using Server.WebApi.Users;

namespace Server.WebApi.Threads
{
    public class ThreadLogic : BaseLogic<Thread>
    {
        private const int ThreadsPerPage = 4;

        public async Task<ThreadViewModel> GetById(ObjectId id)
        {
            var subwebbit = await Database.GetCollection<Subwebbit>().AsQueryable()
                .FirstAsync(x => x.Threads.Contains(id));
            var thread = await Get(id);
            var author = await new UserLogic().GetName(thread.AuthorId);

            return new ThreadViewModel(thread,
                UserSession.UserId,
                author,
                await new SubwebbitLogic().GetById(subwebbit.Id));
        }

        public async Task<IEnumerable<ThreadViewModel>> GetRecentThreads(ObjectId subwebbitId,
            int index)
        {
            var subwebbit = await Database.GetCollection<Subwebbit>().Get(subwebbitId);
            var threadsIds = GetCollection().AsQueryable()
                .Where(x => subwebbit.Threads.Contains(x.Id))
                .OrderByDescending(x => x.Date).Skip(index).Take(ThreadsPerPage)
                .Select(x => x.Id);

            return await GetById(threadsIds);
        }

        public async Task<IEnumerable<ThreadViewModel>> GetTopThreadsFromSubscribed(int index)
        {
            var subwebbitsIds = await new UserLogic().GetSubscribedIds();
            var threadsIds = await Database.GetCollection<Subwebbit>().AsQueryable()
                .Where(x => subwebbitsIds.Contains(x.Id))
                .SelectMany(x => x.Threads).ToListAsync();

            var topThreadsIds = (await GetCollection().AsQueryable()
                    .Where(x => threadsIds.Contains(x.Id)).ToListAsync())
                .OrderByDescending(x => x.Upvoters.Count - x.Downvoters.Count)
                .Skip(index).Take(ThreadsPerPage).Select(x => x.Id);

            return await GetById(topThreadsIds);
        }

        private async Task<IEnumerable<ThreadViewModel>> GetById(IEnumerable<ObjectId> ids)
        {
            var threads = new List<ThreadViewModel>();

            foreach (var id in ids)
            {
                threads.Add(await GetById(id));
            }

            return threads;
        }

        public async Task Create(NewThreadViewModel thread)
        {
            EnsureThreadDetails(thread);
            var newThread = new Thread(thread.Content, UserSession.UserId, DateTime.Now, thread.Title);

            await Create(newThread);

            await Database.GetCollection<Subwebbit>().UpdateOneAsync(
                x => x.Id == ObjectId.Parse(thread.SubwebbitId),
                Builders<Subwebbit>.Update.AddToSet(x => x.Threads, newThread.Id));
        }

        private void EnsureThreadDetails(NewThreadViewModel thread)
        {
            if (!new ThreadValidator().IsValid(thread))
            {
                throw new InvalidModelDetailsException();
            }
        }

        public async Task Delete(ObjectId id)
        {
            var subwebbit = await Database.GetCollection<Subwebbit>().AsQueryable()
                .FirstAsync(x => x.Threads.Contains(id));
            await new SubwebbitLogic().EnsureOwnership(subwebbit.Id, UserSession.UserId);

            await GetCollection().DeleteOneAsync(x => x.Id == id);

            await Database.GetCollection<Subwebbit>().UpdateOneAsync(
                x => x.Id == subwebbit.Id,
                Builders<Subwebbit>.Update.Pull(x => x.Threads, id));
        }

        public async Task<VoteInfoViewModel> Vote(ThreadVoteViewModel threadVote)
        {
            var thread = await Get(ObjectId.Parse(threadVote.Id));
            new ItemVoter(thread, UserSession.UserId).Vote(threadVote.Vote);

            await GetCollection().ReplaceOneAsync(x => x.Id == thread.Id, thread);

            return new VoteInfoViewModel(thread, UserSession.UserId);
        }
    }
}