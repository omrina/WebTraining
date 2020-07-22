using Server.WebApi.Threads.ViewModels;
using Server.WebApi.Validators;

namespace Server.WebApi.Threads.Validation
{
    public class ThreadValidator
    {
        private const int MinTitleLength = 1;
        private const int MaxTitleLength = 200;

        public bool IsValid(NewThreadViewModel thread)
        {
            return !string.IsNullOrWhiteSpace(thread.SubwebbitId) &&
                   new StringValidator().IsLengthBetween(thread.Title, MinTitleLength, MaxTitleLength) &&
                   !string.IsNullOrWhiteSpace(thread.Content);
        }
    }
}