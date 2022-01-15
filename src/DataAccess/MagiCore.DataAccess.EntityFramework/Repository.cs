using MagiCore.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagiCore.DataAccess.EntityFramework;

public abstract class Repository<TEntity, TContext> : IEfRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
{
    public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate) =>
        FindAllHelper(predicate);

    public ICollection<TEntity> FindAll() =>
        FindAllHelper();

    public TEntity? Find(Expression<Func<TEntity, bool>> predicate)
    {
        using var context = new TContext();
        var query = context.Set<TEntity>().AsQueryable();

        return query.FirstOrDefault(predicate);
    }

    public void Add(TEntity entity) =>
        Transaction(entity, EntityState.Added);

    public void Update(TEntity entity) =>
        Transaction(entity, EntityState.Modified);

    public void Delete(TEntity entity) =>
        Transaction(entity, EntityState.Modified);

    public bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        using var context = new TContext();
        return context.Set<TEntity>().Any(predicate);
    }

    public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate) =>
        await FindAllHelperAsync(predicate);

    public async Task<ICollection<TEntity>> FindAllAsync() =>
        await FindAllHelperAsync();

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        await using var context = new TContext();
        var query = context.Set<TEntity>().AsQueryable();

        return await query.FirstOrDefaultAsync(predicate);
    }

    public async Task AddAsync(TEntity entity) =>
        await TransactionAsync(entity, EntityState.Added);

    public async Task UpdateAsync(TEntity entity) =>
        await TransactionAsync(entity, EntityState.Modified);

    public async Task DeleteAsync(TEntity entity) =>
        await TransactionAsync(entity, EntityState.Deleted);

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        await using var context = new TContext();
        return await context.Set<TEntity>().AnyAsync(predicate);
    }

    public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties) =>
        FindAllHelper(predicate, navigationProperties);

    public ICollection<TEntity> FindAll(params string[] navigationProperties) =>
        FindAllHelper(null, navigationProperties);

    public TEntity? Find(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties)
    {
        using var context = new TContext();
        var query = context.Set<TEntity>().AsQueryable();

        query = navigationProperties.Aggregate(query, (current, navigationProperty) =>
            current.Include(navigationProperty));

        return query.FirstOrDefault(predicate);
    }

    public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties) =>
        await FindAllHelperAsync(predicate, navigationProperties);

    public async Task<ICollection<TEntity>> FindAllAsync(params string[] navigationProperties) =>
        await FindAllHelperAsync(null, navigationProperties);

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties)
    {
        await using var context = new TContext();
        var query = context.Set<TEntity>().AsQueryable();

        query = navigationProperties.Aggregate(query, (current, navigationProperty) =>
            current.Include(navigationProperty));

        return await query.FirstOrDefaultAsync(predicate);
    }

    #region Private Methods

    private static ICollection<TEntity> FindAllHelper(Expression<Func<TEntity, bool>>? predicate = null, params string[] navigationProperties)
    {
        using var context = new TContext();
        var query = context.Set<TEntity>().AsQueryable();

        if (predicate is not null) query = query.Where(predicate);

        return navigationProperties.Aggregate(query, (current, navigationProperty) =>
            current.Include(navigationProperty)).ToList();
    }

    private static async Task<ICollection<TEntity>> FindAllHelperAsync(Expression<Func<TEntity, bool>>? predicate = null, params string[] navigationProperties)
    {
        await using var context = new TContext();
        var query = context.Set<TEntity>().AsQueryable();

        if (predicate is not null) query = query.Where(predicate);

        return await navigationProperties.Aggregate(query, (current, navigationProperty) =>
            current.Include(navigationProperty)).ToListAsync();
    }

    private static void Transaction(TEntity entity, EntityState entityState)
    {
        using var context = new TContext();
        context.Entry(entity).State = entityState;
        context.SaveChanges();
    }

    private static async Task TransactionAsync(TEntity entity, EntityState entityState)
    {
        await using var context = new TContext();
        context.Entry(entity).State = entityState;
        await context.SaveChangesAsync();
    }

    #endregion
}

public abstract class Repository<TEntity, TContext, TKey> : Repository<TEntity, TContext>, IEfRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
    where TContext : DbContext, new()
    where TKey : IEquatable<TKey>
{
    public TEntity? FindById(TKey id)
    {
        using var context = new TContext();
        var query = context.Set<TEntity>().AsQueryable();

        return query.FirstOrDefault(x => x.Id.Equals(id));
    }

    public void DeleteById(TKey id) =>
        Delete(new TEntity { Id = id });

    public bool ExistsById(TKey id) =>
        Exists(x => x.Id.Equals(id));

    public async Task<TEntity?> FindByIdAsync(TKey id)
    {
        await using var context = new TContext();
        var query = context.Set<TEntity>().AsQueryable();

        return await query.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task DeleteByIdAsync(TKey id) =>
        await DeleteAsync(new TEntity { Id = id });

    public async Task<bool> ExistsByIdAsync(TKey id) =>
        await ExistsAsync(x => x.Id.Equals(id));

    public TEntity? FindById(TKey id, params string[] navigationProperties)
    {
        using var context = new TContext();
        var query = context.Set<TEntity>().AsQueryable();

        query = navigationProperties.Aggregate(query, (current, navigationProperty) =>
            current.Include(navigationProperty));

        return query.FirstOrDefault(x => x.Id.Equals(id));
    }

    public bool IsPropertiesModified(TEntity entity, string property, params string[] properties)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        using var context = new TContext();
        var tracked = context.Set<TEntity>().SingleOrDefault(x => x.Id.Equals(entity.Id));

        if (tracked is null) throw new ArgumentException("Entity not found.");

        var entry = context.Entry(tracked);
        entry.CurrentValues.SetValues(entity);
        return properties.Append(property).Any(x => !Equals(entry.CurrentValues[x], entry.OriginalValues[x]));
    }

    public async Task<TEntity?> FindByIdAsync(TKey id, params string[] navigationProperties)
    {
        await using var context = new TContext();
        var query = context.Set<TEntity>().AsQueryable();

        query = navigationProperties.Aggregate(query, (current, navigationProperty) =>
            current.Include(navigationProperty));

        return await query.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<bool> IsPropertiesModifiedAsync(TEntity entity, string property, params string[] properties)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        await using var context = new TContext();
        var tracked = await context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));

        if (tracked is null) throw new ArgumentException("Entity not found.");

        var entry = context.Entry(tracked);
        entry.CurrentValues.SetValues(entity);
        return properties.Append(property).Any(x => !Equals(entry.CurrentValues[x], entry.OriginalValues[x]));
    }
}