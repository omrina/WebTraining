using System.Collections.Generic;
using MongoDB.Driver;
using Server.Models;

namespace Server.BL.Ratings
{
    public class UpdateOperationDto
    {
        public FilterDefinition<Subwebbit> Filter { get; set; }
        // TODO: RENAME item
        public string Item { get; set; }
        public UpdateOptions Options { get; set; }

        public UpdateOperationDto(FilterDefinition<Subwebbit> filter,
                                  string item,
                                  IEnumerable<ArrayFilterDefinition> arrayFilters)
        {
            Filter = filter;
            Item = item;
            Options = new UpdateOptions { ArrayFilters = arrayFilters };
        }
    }
}