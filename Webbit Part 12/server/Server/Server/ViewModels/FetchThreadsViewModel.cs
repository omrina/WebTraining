namespace Server.ViewModels
{
    public class FetchThreadsViewModel
    {
        public string SubwebbitId { get; set; }
        public int Index { get; set; }
        public int Amount { get; set; }

        public FetchThreadsViewModel(string subwebbitId, int index, int amount)
        {
            SubwebbitId = subwebbitId;
            Index = index;
            Amount = amount;
        }
    }
}