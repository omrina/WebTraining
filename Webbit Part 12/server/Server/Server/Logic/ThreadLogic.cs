using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using Server.Exceptions;
using Server.Models;
using Server.ViewModels;

namespace Server.Logic
{
    public class ThreadLogic : BaseLogic<Thread>
    {
        // public async Task<ThreadViewModel> Get(string subwebbitId, string userId)
        // {
        //     var subwebbit = await Get(subwebbitId).SingleOrDefaultAsync();
        //
        //     if (subwebbit == null)
        //     {
        //         throw new SubwebbitNotFoundException();
        //     }
        //
        //     var isSubscribed = await new UserLogic().IsSubscribed(userId, subwebbitId);
        //
        //     return new ThreadViewModel(subwebbit, isSubscribed);
        // }
    }
}