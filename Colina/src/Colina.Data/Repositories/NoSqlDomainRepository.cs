﻿using Colina.Data.Settings;
using Colina.Language.Domain.Repositories;
using Colina.Models.Abstraction.DataTransferObjects;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Colina.Data.Repositories
{
    public class NoSqlDomainRepository : IDomainRepository
    {
        private readonly IMongoDatabase _database;
        private readonly IMemoryCache _cache;

        public NoSqlDomainRepository(
            NoSqlSettings settings, 
            IMemoryCache cache)
        {
            _cache = cache;

            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase("colina");
        }

        public void CreateDataSetCache()
        {
            var commandsCollection = _database.GetCollection<CommandDto>("commands");
            var commandsTask = commandsCollection.Find(Builders<CommandDto>.Filter.Empty).ToListAsync();

            var imagesCollection = _database.GetCollection<ImageDto>("images");
            var imagesTask = imagesCollection.Find(Builders<ImageDto>.Filter.Empty).ToListAsync();

            Task.WaitAll(commandsTask, imagesTask);

            var commands = commandsTask.Result;
            var images = imagesTask.Result;

            _cache.Set("commands", commands);
            _cache.Set("images", images);            
            
            //var query = Query<Command>.Exists(c => !string.IsNullOrEmpty(c.PtBR));
        }
    }
}
