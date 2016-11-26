using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Data.Repositories.DataTransfersObjects
{
    public class EnvironmentDto
    {
        public ObjectId Id { get; protected set; }
        [BsonElement("session_id")]
        public Guid SessionId { get; set; }
        // TODO: Objects of environment
        public int Objects { get; set; }
    }
}
