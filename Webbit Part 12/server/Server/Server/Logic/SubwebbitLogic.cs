using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Models;
using Server.ViewModels;

namespace Server.Logic
{
    public class SubwebbitLogic : BaseLogic<Subwebbit>
    {
        public IEnumerable<SearchedSubwebbitViewModel> GetAllByNameMatch(string name)
        {
            var matchingSubwebbits = GetAll().Where(x => x.Name.Contains(name)).ToList();

            return matchingSubwebbits.Select(x => new SearchedSubwebbitViewModel(x));
        }
    }
}