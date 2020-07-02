using System.Collections.Generic;
using Server.Models;

namespace Server.ViewModels
{
    public class SubwebbitViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        // TODO: change to thread viewModel???
        public IEnumerable<Thread> Threads { get; set; }
        public long SubscribersCount { get; set; }

        public SubwebbitViewModel(Subwebbit subwebbit)
        {
            Id = subwebbit.Id.ToString();
            Name = subwebbit.Name;
            Threads = subwebbit.Threads;
            SubscribersCount = subwebbit.SubscribersCount;
        }
    }
}