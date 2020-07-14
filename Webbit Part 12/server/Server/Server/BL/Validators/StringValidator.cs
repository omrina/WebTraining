namespace Server.BL.Validators
{
    public class StringValidator
    {
        public bool IsLengthBetween(string s, int min, int max)
        {
            return !string.IsNullOrWhiteSpace(s) && s.Length >= min && s.Length <= max;
        }
    }
}