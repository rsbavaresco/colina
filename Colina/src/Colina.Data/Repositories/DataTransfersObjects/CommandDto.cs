using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Data.Repositories.DataTransfersObjects
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
