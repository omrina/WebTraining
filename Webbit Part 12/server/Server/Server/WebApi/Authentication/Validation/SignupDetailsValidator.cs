using Server.WebApi.Authentication.ViewModels;
using Server.WebApi.Validators;

namespace Server.WebApi.Authentication.Validation
{
    public class SignupDetailsValidator
    {
        public bool IsValid(UserAuthViewModel user)
        {
            var stringValidator = new StringValidator();

            return stringValidator.IsLengthBetween(user.Username, 4, 14) &&
                   user.Password.Length >= 8;
        }
    }
}