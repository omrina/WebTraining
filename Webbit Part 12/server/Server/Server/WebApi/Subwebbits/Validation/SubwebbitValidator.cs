using Server.WebApi.Validators;

namespace Server.WebApi.Subwebbits.Validation
{
    public class SubwebbitValidator
    {
        private const int MinNameLength = 1;
        private const int MaxNameLength = 30;

        public bool IsValid(string name)
        {
            return !string.IsNullOrWhiteSpace(name) &&
                    new StringValidator().IsLengthBetween(name, MinNameLength, MaxNameLength);
        }
    }
}