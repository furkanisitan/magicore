using MagiCore.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace MagiCore.DataAccess.Mongo;

public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    private readonly IMongoCollection<TEntity> _collection;

    protected Repository(MongoOptions options, string collectionNameKey)
    {
        var client = new MongoClient(options.ConnectionString);
        var database = client.GetDatabase(options.DatabaseName);

        if (options.CollectionNames is null)
            throw new ArgumentNullException(nameof(options.CollectionNames), "The argument cannot be null.");

        _collection = database.GetCollection<TEntity>(options.CollectionNames[collectionNameKey]);
    }

    public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate) =>
        _collection.Find(predicate).ToList();

    public ICollection<TEntity> FindAll() =>
        FindAll(x => true);

    public TEntity? Find(Expression<Func<TEntity, bool>> predicate) =>
        _collection.Find(predicate).FirstOrDefault();

    public void Add(TEntity entity) =>
        _collection.InsertOne(entity);

    public void Update(TEntity entity) =>
        _collection.ReplaceOne(x => x.Id.Equals(entity.Id), entity);

    public void Delete(TEntity entity) =>
        _collection.DeleteOne(x => x.Id.Equals(entity.Id));

    public bool Exists(Expression<Func<TEntity, bool>> predicate) =>
        _collection.AsQueryable().Any(predicate);

    public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate) =>
        await (await _collection.FindAsync(predicate)).ToListAsync();

    public async Task<ICollection<TEntity>> FindAllAsync() =>
        await FindAllAsync(x => true);

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
        await (await _collection.FindAsync(predicate)).FirstOrDefaultAsync();

    public async Task AddAsync(TEntity entity) =>
        await _collection.InsertOneAsync(entity);

    public async Task UpdateAsync(TEntity entity) =>
        await _collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity);

    public async Task DeleteAsync(TEntity entity) =>
        await _collection.DeleteOneAsync(x => x.Id.Equals(entity.Id));

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) =>
        await _collection.AsQueryable().AnyAsync(predicate);

    public TEntity? FindById(TKey id) =>
        Find(x => x.Id.Equals(id));

    public void DeleteById(TKey id) =>
        Delete(new TEntity { Id = id });

    public bool ExistsById(TKey id) =>
        Exists(x => x.Id.Equals(id));

    public async Task<TEntity?> FindByIdAsync(TKey id) =>
        await FindAsync(x => x.Id.Equals(id));

    public async Task DeleteByIdAsync(TKey id) =>
        await DeleteAsync(new TEntity { Id = id });

    public async Task<bool> ExistsByIdAsync(TKey id) =>
        await ExistsAsync(x => x.Id.Equals(id));
}