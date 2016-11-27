using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Colina.Data.Repositories.DataTransfersObjects
{
    public class EnvironmentItemDto
    {
        protected EnvironmentItemDto()
        {

        }

        [BsonElement("imageUniqueId")]
        protected string MongoImageUniqueId { get; set; }
        public Guid ImageUniqueId
        {
            get
            {
                return Guid.Parse(MongoImageUniqueId);
            }
        }

        [BsonElement("positionX")]
        public int PositionX { get; set; }

        [BsonElement("positionY")]
        public int PositionY { get; set; }

        public static EnvironmentItemDto Create(Guid imageUniqueId, int x, int y)
        {
            return new EnvironmentItemDto
            {
                MongoImageUniqueId = imageUniqueId.ToString(),
                PositionX = x,
                PositionY = y
            };
        }
    }
}
