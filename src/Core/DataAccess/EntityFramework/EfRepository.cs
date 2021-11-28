using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public abstract class EfRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties) =>
            FindAllHelper(predicate, navigationProperties);

        public ICollection<TEntity> FindAll(params string[] navigationProperties) =>
            FindAllHelper(null, navigationProperties);

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties)
        {
            using var context = new TContext();
            var query = context.Set<TEntity>().AsQueryable();

            query = navigationProperties.Aggregate(query, (current, includeProperty) =>
                current.Include(includeProperty));

            return query.FirstOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            using var context = new TContext();
            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            using var context = new TContext();
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using var context = new TContext();
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            using var context = new TContext();
            return context.Set<TEntity>().Any(predicate);
        }

        #region Private Methods

        private ICollection<TEntity> FindAllHelper(Expression<Func<TEntity, bool>> predicate = null, params string[] navigationProperties)
        {
            using var context = new TContext();
            var query = context.Set<TEntity>().AsQueryable();

            if (predicate != null) query = query.Where(predicate);

            return navigationProperties.Aggregate(query, (current, includeProperty) =>
                current.Include(includeProperty)).ToList();
        }

        #endregion
    }

    public abstract class EfRepository<TEntity, TContext, TKey> : EfRepository<TEntity, TContext>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TContext : DbContext, new()
        where TKey : IEquatable<TKey>
    {
        public TEntity FindById(TKey id, params string[] navigationProperties)
        {
            using var context = new TContext();
            var query = context.Set<TEntity>().AsQueryable();

            query = navigationProperties.Aggregate(query, (current, includeProperty) =>
                current.Include(includeProperty));

            return query.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void DeleteById(TKey id)
        {
            using var context = new TContext();
            context.Entry(new TEntity { Id = id }).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public bool ExistsById(TKey id)
        {
            using var context = new TContext();
            return context.Set<TEntity>().Any(x => x.Id.Equals(id));
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
