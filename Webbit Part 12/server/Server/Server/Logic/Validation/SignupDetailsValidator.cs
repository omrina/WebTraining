using Server.ViewModels;

namespace Server.Logic.Validation
{
    public class SignupDetailsValidator
    {
        public bool IsValid(UserSignupViewModel user)
        {
            var stringValidator = new StringValidator();

            return stringValidator.IsLengthBetween(user.Username, 4, 14) &&
                   stringValidator.IsLengthBetween(user.Password, 8, 20);
        }
    }
}