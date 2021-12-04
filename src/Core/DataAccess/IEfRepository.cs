using Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Core.DataAccess;

/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IEfRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    /// <summary>
    /// Finds all <typeparamref name="TEntity"/> elements by the specified <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">A function that defines the conditions of the elements to search for.</param>
    /// <param name="navigationProperties">Names of navigation properties to be included.</param>
    /// <returns>A <see cref="ICollection{T}"/> that contains <typeparamref name="TEntity"/> elements.</returns>
    ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties);

    /// <summary>
    /// Finds all <typeparamref name="TEntity"/> elements.
    /// </summary>
    /// <param name="navigationProperties">Names of navigation properties to be included.</param>
    /// <returns>A <see cref="ICollection{T}"/> that contains <typeparamref name="TEntity"/> elements.</returns>
    ICollection<TEntity> FindAll(params string[] navigationProperties);

    /// <summary>
    /// Finds an <typeparamref name="TEntity"/> element by the specified <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">A function that defines the conditions of the elements to search for.</param>
    /// <param name="navigationProperties">Names of navigation properties to be included.</param>
    /// <returns>The single <typeparamref name="TEntity"/> element that satisfied a specified predicate or <see langword="null"/> if no such element is found.</returns>
    TEntity Find(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties);
}
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type used for the primary key.</typeparam>
public interface IEfRepository<TEntity, in TKey> : IEfRepository<TEntity>, IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Finds an <typeparamref name="TEntity"/> element by <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The identifier of <typeparamref name="TEntity"/> element.</param>
    /// <param name="navigationProperties">Names of navigation properties to be included.</param>
    /// <returns>The single <typeparamref name="TEntity"/> element that satisfied a specified predicate or <see langword="null"/> if no such element is found.</returns>
    TEntity FindById(TKey id, params string[] navigationProperties);

    /// <summary>
    /// Checks whether the specified <paramref name="properties"/> of the <paramref name="entity"/> change according to the values in the database.
    /// </summary>
    /// <param name="entity">The entity element to check.</param>
    /// <param name="property">>The name of the property to check.</param>
    /// <param name="properties">The names of the properties to check.</param>
    /// <returns><see langword="true"/> if the value of any properties has modified, otherwise <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws when <paramref name="entity"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Throws when <paramref name="entity"/> not found.</exception>
    bool IsPropertiesModified([NotNull] TEntity entity, string property, params string[] properties);
}