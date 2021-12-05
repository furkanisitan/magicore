﻿namespace Core.Utilities.Mapping;

public interface IMapperCore
{
    /// <summary>
    /// Execute a mapping from the source object to a new destination object.
    /// </summary>
    /// <typeparam name="TDestination">Destination type to create.</typeparam>
    /// <param name="source">Source object to map from.</param>
    /// <returns>Mapped destination object.</returns>
    TDestination Map<TDestination>(object source);

    /// <summary>
    /// Execute a mapping from the source object to a new destination object.
    /// </summary>
    /// <typeparam name="TSource">Source type to map from.</typeparam>
    /// <typeparam name="TDestination">Destination type to create.</typeparam>
    /// <param name="source">Source object to map from.</param>
    /// <returns>Mapped destination object.</returns>
    TDestination Map<TSource, TDestination>(TSource source);

}