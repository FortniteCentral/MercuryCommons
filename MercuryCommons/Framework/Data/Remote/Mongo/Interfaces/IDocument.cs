using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MercuryCommons.Framework.Data.Remote.Mongo.Interfaces;

public interface IDocument
{
    [BsonId, BR(BsonType.String)] ObjectId Id { get; set; }
    DateTime CreatedAt { get; }
}