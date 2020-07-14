using Server.BL.Threads.ViewModels;
using Server.BL.Validators;

namespace Server.BL.Threads.Validation
{
    public class ThreadValidator
    {
        public bool IsValid(NewThreadViewModel thread)
        {
            return !string.IsNullOrWhiteSpace(thread.SubwebbitId) &&
                   new StringValidator().IsLengthBetween(thread.Title, 1, 200) &&
                   !string.IsNullOrWhiteSpace(thread.Content) &&
                   !string.IsNullOrWhiteSpace(thread.Author);
        }
    }
}