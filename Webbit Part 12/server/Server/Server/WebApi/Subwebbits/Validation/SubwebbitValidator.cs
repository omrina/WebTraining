using Server.WebApi.Validators;

namespace Server.WebApi.Subwebbits.Validation
{
    public class SubwebbitValidator
    {
        public bool IsValid(string name)
        {
            return !string.IsNullOrWhiteSpace(name) &&
                    new StringValidator().IsLengthBetween(name, 1, 30);
        }
    }
}