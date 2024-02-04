using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace MercuryCommons.Framework.Data.Remote.Mongo.Interfaces;

public interface IMongoRepository<TDocument> where TDocument : IDocument
{
    IQueryable<TDocument> AsQueryable();
    Task<List<TDocument>> ToList();
    IEnumerable<TDocument> FilterBy(FilterDefinition<TDocument> filterExpression);
    IEnumerable<TProjected> FilterBy<TProjected>(FilterDefinition<TDocument> filterExpression, ProjectionDefinition<TDocument, TProjected> projectionExpression);
    TDocument FindOne(FilterDefinition<TDocument> filterExpression);
    Task<TDocument> FindOneAsync(FilterDefinition<TDocument> filterExpression);
    TDocument FindById(ObjectId id);
    Task<TDocument> FindByIdAsync(ObjectId id);
    TDocument FindById(string id);
    Task<TDocument> FindByIdAsync(string id);
    void InsertOne(TDocument document);
    Task InsertOneAsync(TDocument document);
    void InsertMany(IEnumerable<TDocument> documents);
    Task InsertManyAsync(IEnumerable<TDocument> documents);
    void UpdateOne(FilterDefinition<TDocument> filterExpression, UpdateDefinition<TDocument> updateExpression, bool upsert = false);
    Task UpdateOneAsync(FilterDefinition<TDocument> filterExpression, UpdateDefinition<TDocument> updateExpression, bool upsert = false);
    void ReplaceOne(TDocument document);
    Task ReplaceOneAsync(TDocument document);
    void DeleteOne(FilterDefinition<TDocument> filterExpression);
    Task DeleteOneAsync(FilterDefinition<TDocument> filterExpression);
    void DeleteById(string id);
    Task DeleteByIdAsync(string id);
    void DeleteMany(FilterDefinition<TDocument> filterExpression);
    Task DeleteManyAsync(FilterDefinition<TDocument> filterExpression);
    Task<ObjectId> UploadFileAsync(byte[] data, string fileName);
    Task<byte[]> RetrieveFileAsync(ObjectId id);
    public IAsyncCursor<GridFSFileInfo<ObjectId>> GetFileInfo(ObjectId id);
    public Task<IAsyncCursor<GridFSFileInfo<ObjectId>>> GetFileInfoAsync(ObjectId id);
}