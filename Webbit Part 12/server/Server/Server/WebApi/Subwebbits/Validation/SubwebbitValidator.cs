using Server.WebApi.Subwebbits.ViewModels;
using Server.WebApi.Validators;

namespace Server.WebApi.Subwebbits.Validation
{
    public class SubwebbitValidator
    {
        public bool IsValid(NewSubwebbitViewModel subwebbit)
        {
            return !string.IsNullOrWhiteSpace(subwebbit.OwnerId) &&
                   new StringValidator().IsLengthBetween(subwebbit.Name, 1, 30);
        }
    }
}