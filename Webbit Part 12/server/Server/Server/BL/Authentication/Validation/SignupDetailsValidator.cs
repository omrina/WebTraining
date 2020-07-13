using Server.BL.Authentication.ViewModels;
using Server.BL.Validators;

namespace Server.BL.Authentication.Validation
{
    public class SignupDetailsValidator
    {
        public bool IsValid(UserSignupViewModel user)
        {
            var stringValidator = new StringValidator();

            return stringValidator.IsLengthBetween(user.Username, 4, 14) &&
                   user.Password.Length >= 8;
        }
    }
}