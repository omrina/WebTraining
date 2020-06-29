namespace Server.Logic.Validation
{
    public class StringValidator
    {
        public bool IsLengthBetween(string s, int min, int max)
        {
            return s.Length >= min && s.Length <= max;
        }
    }
}