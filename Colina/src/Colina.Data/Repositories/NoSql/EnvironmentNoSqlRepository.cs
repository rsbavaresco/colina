using Colina.Data.Repositories.DataTransfersObjects;
using Colina.Data.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace Colina.Data.Repositories.NoSql
{
    public class EnvironmentNoSqlRepository
    {
        private readonly IMongoCollection<EnvironmentDto> _environmentsCollection;

        public EnvironmentNoSqlRepository(NoSqlSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase("colina");
            _environmentsCollection = database.GetCollection<EnvironmentDto>("environments");
        }

        public EnvironmentDto GetByUserSession(Guid session)
        {
            if (session.Equals(Guid.Empty))
                throw new ArgumentException(nameof(session));

            var filter = Builders<EnvironmentDto>.Filter.Eq("sessionId", session.ToString());
            var environment = _environmentsCollection.Find(filter).SingleOrDefault();

            return environment;
        }

        public void Insert(EnvironmentDto environment)
        {
            if (environment == null)
                throw new ArgumentNullException(nameof(environment));

            _environmentsCollection.InsertOne(environment);
        }

        public void Update(EnvironmentDto environment)
        {
            if (environment == null)
                throw new ArgumentNullException(nameof(environment));

            var filter = Builders<EnvironmentDto>.Filter.Eq("sessionId", environment.SessionId.ToString());
            var update = Builders<EnvironmentDto>.Update
                            .Set("sessionId", environment.SessionId.ToString())
                            .Set("items", environment.Items);
            _environmentsCollection.UpdateOne(filter, update);
        }
    }
}
