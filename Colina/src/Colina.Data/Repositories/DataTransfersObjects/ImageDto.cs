﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Data.Repositories.DataTransfersObjects
{
    public class ImageDto
    {
        public ObjectId Id { get; set; }
        [BsonElement("uniqueId")]
        public Guid UniqueId { get; set; }
        [BsonElement("path")]
        public string Path { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("width")]
        public float Width { get; set; }
        [BsonElement("height")]
        public float Height { get; set; }
        [BsonElement("pt-BR")]
        public string PtBR { get; set; }
        [BsonElement("en-US")]
        public string EnUS { get; set; }
    }
}
