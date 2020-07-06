using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Server.Exceptions;
using Server.Logic.Validation;
using Server.Models;
using Server.ViewModels;

namespace Server.Logic
{    
    public class AuthLogic : BaseLogic<User>
    {
        public UserViewModel Login(LoginViewModel loginViewModel)
        {
            var user =  Collection.AsQueryable().SingleOrDefault(x =>
                x.Password == loginViewModel.Password &&
                x.Username == loginViewModel.Username);
            
            if (user == null)
            {
                throw new LoginFailedException();
            }

            return new UserViewModel(user);
        }

        public async Task Signup(UserSignupViewModel user)
        {
            EnsureSignupDetails(user);

            try
            {
                await Collection.InsertOneAsync(new User(user.Username, user.Password));
            }
            catch (MongoWriteException e) when(e.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                // LOG HERE
                throw new UsernameAlreadyTakenException();
            }
        }

        private void EnsureSignupDetails(UserSignupViewModel user)
        {
            if (!new SignupDetailsValidator().IsValid(user))
            {
                throw new InvalidSignupDetailsException();
            }
        }
    }
}