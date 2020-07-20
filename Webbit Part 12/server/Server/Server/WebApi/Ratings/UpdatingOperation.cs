using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Models;

namespace Server.WebApi.Ratings
{
    public class UpdatingOperation
    {
        private FilterDefinitionBuilder<Thread> FilterBuilder => Builders<Thread>.Filter;
        private List<ArrayFilterDefinition> ArrayFilter { get; }
        public FilterDefinition<Thread> Filter { get; set; }
        public string ModelHierarchy { get; set; }
        public UpdateOptions Options => new UpdateOptions { ArrayFilters = ArrayFilter };

        public UpdatingOperation(string threadId)
        {
            ArrayFilter = new List<ArrayFilterDefinition>();
            Filter = GetThreadFilterDefinition(threadId);
            ModelHierarchy = $"{nameof(Subwebbit.Threads)}.$";
        }

        public FilterDefinition<Thread> GetThreadFilterDefinition(string threadId)
        {
            // TODO: maybe filter prop is not necessary?
            return FilterBuilder.Where(x => x.Id == ObjectId.Parse(threadId));
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