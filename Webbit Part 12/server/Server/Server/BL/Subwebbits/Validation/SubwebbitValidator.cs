using Server.BL.Subwebbits.ViewModels;
using Server.BL.Validators;

namespace Server.BL.Subwebbits.Validation
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