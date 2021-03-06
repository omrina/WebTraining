﻿using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using Server.BL.Authentication.Validation;
using Server.BL.Authentication.ViewModels;
using Server.BL.Subwebbits;
using Server.BL.Users;
using Server.Exceptions;
using Server.Models;

namespace Server.BL.Authentication
{    
    public class AuthLogic : BaseLogic<User>
    {
        public async Task<UserViewModel> Login(UserAuthViewModel loginViewModel)
        {
            var user =  await GetAll().SingleOrDefaultAsync(x =>
                x.Password == loginViewModel.Password &&
                x.Username == loginViewModel.Username);
            
            if (user == null)
            {
                throw new LoginFailedException();
            }

            return new UserViewModel(user);
        }

        public async Task Signup(UserAuthViewModel user)
        {
            const int subscriptionsOnSignup = 10;

            EnsureSignupDetails(user);
            await EnsureUsernameNotTaken(user.Username);

            var newUser = new User(user.Username, user.Password);

            await Collection.InsertOneAsync(newUser);
            await SubscribeToTopSubwebbits(newUser.Id, subscriptionsOnSignup);
        }

        private void EnsureSignupDetails(UserAuthViewModel user)
        {
            if (!new SignupDetailsValidator().IsValid(user))
            {
                throw new InvalidModelDetailsException();
            }
        }

        private async Task EnsureUsernameNotTaken(string username)
        {
            if (await new UserLogic().IsUsernameExists(username))
            {
                throw new UsernameAlreadyTakenException();
            }
        }

        private async Task SubscribeToTopSubwebbits(ObjectId userId, int amount)
        {
            var userLogic = new UserLogic {UserId = userId};
            var subwebbitsIds = await new SubwebbitSubscriptionLogic().GetMostSubscribed(amount);

            foreach (var id in subwebbitsIds)
            {
                await userLogic.Subscribe(id.ToString());
            }
        }
    }
}