namespace Server.WebApi.Validators
{
    public class StringValidator
    {
        public bool IsLengthBetween(string input, int min, int max)
        {
            return input.Length >= min && input.Length <= max;
        }
    }
}