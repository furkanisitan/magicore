using Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.Mongo;

public abstract class MongoRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    private readonly IMongoCollection<TEntity> _collection;

    protected MongoRepository(MongoOptions options, string collectionNameKey)
    {
        var client = new MongoClient(options.ConnectionString);
        var database = client.GetDatabase(options.DatabaseName);
        _collection = database.GetCollection<TEntity>(options.CollectionNames[collectionNameKey]);
    }

    public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate) =>
        _collection.Find(predicate).ToList();

    public ICollection<TEntity> FindAll() =>
        FindAll(x => true);

    public TEntity Find(Expression<Func<TEntity, bool>> predicate) =>
        _collection.Find(predicate).FirstOrDefault();

    public void Add(TEntity entity) =>
        _collection.InsertOne(entity);

    public void Update(TEntity entity) =>
        _collection.ReplaceOne(x => x.Id.Equals(entity.Id), entity);

    public void Delete(TEntity entity) =>
        _collection.DeleteOne(x => x.Id.Equals(entity.Id));

    public bool Exists(Expression<Func<TEntity, bool>> predicate) =>
        _collection.AsQueryable().Any(predicate);

    public TEntity FindById(TKey id) =>
        Find(x => x.Id.Equals(id));

    public void DeleteById(TKey id) =>
        Delete(new TEntity { Id = id });

    public bool ExistsById(TKey id) =>
        Exists(x => x.Id.Equals(id));
}