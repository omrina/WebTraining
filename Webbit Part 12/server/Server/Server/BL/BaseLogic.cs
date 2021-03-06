﻿using System;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server.Models;

namespace Server.BL
{
    public abstract class BaseLogic<TModel> where TModel : BaseModel
    {
        public ObjectId UserId { get; set; }
        protected IMongoCollection<TModel> Collection { get; }
        protected UpdateDefinitionBuilder<TModel> UpdateBuilder => Builders<TModel>.Update;

        protected Expression<Func<T, bool>>  GenerateByIdFilter<T>(string id) where T : BaseModel
        {
            return x => x.Id == ObjectId.Parse(id);
        }

        protected BaseLogic()
        {
            Collection = new MongoConnection().GetCollection<TModel>(typeof(TModel).Name.ToLower());
        }

        protected IMongoQueryable<TModel> GetAll()
        {
            return Collection.AsQueryable();
        }

        protected IMongoQueryable<TModel> Get(string id)
        {
            return GetAll().Where(GenerateByIdFilter<TModel>(id));
        }
    }
}