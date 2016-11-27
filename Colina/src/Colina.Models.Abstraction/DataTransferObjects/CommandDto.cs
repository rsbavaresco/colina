using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Colina.Models.Abstraction.DataTransferObjects
{
    public class CommandDto
    {        
        public ObjectId Id { get; set; }
        [BsonElement("pt-BR")]
        public string PtBR { get; set; }
        [BsonElement("en-US")]
        public string EnUS { get; set; }
    }
}
