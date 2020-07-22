using Server.WebApi.Authentication.ViewModels;
using Server.WebApi.Validators;

namespace Server.WebApi.Authentication.Validation
{
    public class SignupDetailsValidator
    {
        private const int MinUsernameLength = 4;
        private const int MaxUsernameLength = 14;
        // TODO: change to regex string
        private const int MinUPasswordLength = 8;


        public bool IsValid(UserAuthViewModel user)
        {
            var stringValidator = new StringValidator();

            return stringValidator.IsLengthBetween(user.Username, MinUsernameLength, MaxUsernameLength) &&
                   user.Password.Length >= MinUPasswordLength;
        }
    }
}