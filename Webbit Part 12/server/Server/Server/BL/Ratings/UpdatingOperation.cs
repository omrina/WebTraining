using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Models;

namespace Server.BL.Ratings
{
    public class UpdatingOperation
    {
        private FilterDefinitionBuilder<Subwebbit> FilterBuilder => Builders<Subwebbit>.Filter;
        private List<ArrayFilterDefinition> ArrayFilter { get; }
        public FilterDefinition<Subwebbit> Filter { get; set; }
        public string ModelHierarchy { get; set; }
        public UpdateOptions Options => new UpdateOptions { ArrayFilters = ArrayFilter };

        public UpdatingOperation(string subwebbitId, string threadId)
        {
            ArrayFilter = new List<ArrayFilterDefinition>();
            Filter = GetThreadFilterDefinition(subwebbitId, threadId);
            ModelHierarchy = $"{nameof(Subwebbit.Threads)}.$";
        }

        public FilterDefinition<Subwebbit> GetThreadFilterDefinition(string subwebbitId,
                                                                     string threadId)
        {
            return FilterBuilder.And(
                FilterBuilder.Where(x => x.Id == ObjectId.Parse(subwebbitId)),
                FilterBuilder.ElemMatch(x => x.Threads,
                    x => x.Id == ObjectId.Parse(threadId)));
        }

        public void AddNestedCommentFilter(ObjectId commentId)
        {
            var commentFilterName = $"{nameof(Thread.Comments).ToLower()}{ArrayFilter.Count}";

            ModelHierarchy += $".{nameof(Thread.Comments).ToLower()}.$[{commentFilterName}]";
            ArrayFilter.Add((ArrayFilterDefinition<BsonDocument>)new BsonDocument($"{commentFilterName}._id",
                new BsonDocument("$eq", commentId)));
        }
    }
}