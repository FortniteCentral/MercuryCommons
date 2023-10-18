using System;
using MercuryCommons.Framework.Data.Remote.Mongo.Interfaces;
using MongoDB.Bson;

namespace MercuryCommons.Framework.Data.Remote.Mongo;

public abstract class Document : IDocument
{
    public ObjectId Id { get; set; }

    public DateTime CreatedAt => Id.CreationTime;
}