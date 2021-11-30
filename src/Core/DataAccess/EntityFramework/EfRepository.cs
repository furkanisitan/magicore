using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public abstract class EfRepository<TEntity, TContext> : IEfRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate) =>
             FindAllHelper(predicate);

        public ICollection<TEntity> FindAll() =>
            FindAllHelper();

        public TEntity Find(Expression<Func<TEntity, bool>> predicate) =>
            Find(predicate, Array.Empty<string>());

        public void Add(TEntity entity) =>
            Transaction(entity, EntityState.Added);

        public void Update(TEntity entity) =>
            Transaction(entity, EntityState.Modified);

        public void Delete(TEntity entity) =>
            Transaction(entity, EntityState.Deleted);

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            using var context = new TContext();
            return context.Set<TEntity>().Any(predicate);
        }

        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties) =>
            FindAllHelper(predicate, navigationProperties);

        public ICollection<TEntity> FindAll(params string[] navigationProperties) =>
            FindAllHelper(null, navigationProperties);

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties)
        {
            using var context = new TContext();
            var query = context.Set<TEntity>().AsQueryable();

            query = navigationProperties.Aggregate(query, (current, navigationProperty) =>
                current.Include(navigationProperty));

            return query.FirstOrDefault(predicate);
        }

        #region Private Methods

        private ICollection<TEntity> FindAllHelper(Expression<Func<TEntity, bool>> predicate = null, params string[] navigationProperties)
        {
            using var context = new TContext();
            var query = context.Set<TEntity>().AsQueryable();

            if (predicate != null) query = query.Where(predicate);

            return navigationProperties.Aggregate(query, (current, navigationProperty) =>
                current.Include(navigationProperty)).ToList();
        }

        private void Transaction(TEntity entity, EntityState entityState)
        {
            using var context = new TContext();
            context.Entry(entity).State = entityState;
            context.SaveChanges();
        }

        #endregion
    }

    public abstract class EfRepository<TEntity, TContext, TKey> : EfRepository<TEntity, TContext>, IEfRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TContext : DbContext, new()
        where TKey : IEquatable<TKey>
    {
        public TEntity FindById(TKey id) =>
            FindById(id, Array.Empty<string>());

        public void DeleteById(TKey id) =>
            Delete(new TEntity { Id = id });

        public bool ExistsById(TKey id) =>
            Exists(x => x.Id.Equals(id));

        public TEntity FindById(TKey id, params string[] navigationProperties)
        {
            using var context = new TContext();
            var query = context.Set<TEntity>().AsQueryable();

            query = navigationProperties.Aggregate(query, (current, navigationProperty) =>
                current.Include(navigationProperty));

            return query.FirstOrDefault(x => x.Id.Equals(id));
        }

        public bool IsPropertiesModified(TEntity entity, string property, params string[] properties)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using var context = new TContext();
            var tracked = context.Set<TEntity>().SingleOrDefault(x => x.Id.Equals(entity.Id));

            if (tracked == null) throw new ArgumentException("Entity not found.");

            var entry = context.Entry(tracked);
            entry.CurrentValues.SetValues(entity);
            return properties.Append(property).Any(x => !entry.OriginalValues[x].Equals(entry.CurrentValues[x]));
        }
    }
}
