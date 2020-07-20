using System.Threading.Tasks;
using MongoDB.Bson;

namespace Server.WebApi.Authentication
{
    public class UserSession
    {
        public static ObjectId UserId { get; private set; }

        public async Task SetToken(ObjectId token)
        {
            UserId = await new AuthLogic().GetUserId(token);
        }
    }
}