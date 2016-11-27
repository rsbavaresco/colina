using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Colina.Models.Abstraction.DataTransferObjects
{
    public class EnvironmentDto
    {
        protected EnvironmentDto()
        {

        }

        [BsonId]
        public ObjectId Id { get; protected set; }

        [BsonElement("sessionId")]
        protected string MongoSessionId { get; set; }

        public Guid SessionId
        {
            get
            {
                return Guid.Parse(MongoSessionId);
            }
        }

        [BsonElement("items")]
        public List<EnvironmentItemDto> Items { get; set; }

        public static EnvironmentDto Create(Guid sessionId)
        {
            return new EnvironmentDto
            {
                Id = ObjectId.GenerateNewId(),
                MongoSessionId = sessionId.ToString(),
                Items = new List<EnvironmentItemDto>()
            };
        }

        public void AddItem(EnvironmentItemDto item)
        {
            Items.Add(item);
        }

        public void RemoveItem(EnvironmentItemDto item)
        {
            Items.Remove(item);
        }
    }
}
