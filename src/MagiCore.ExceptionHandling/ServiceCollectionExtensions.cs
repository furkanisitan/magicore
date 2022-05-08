using MagiCore.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MagiCore.ExceptionHandling;

public static class ServiceCollectionExtensions
{
    /// <inheritdoc cref="AddExceptionHandler(IServiceCollection, IEnumerable{Assembly}?, bool, ServiceLifetime )"/>
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services) =>
        AddExceptionHandlerClasses(services, null);

    /// <inheritdoc cref="AddExceptionHandler(IServiceCollection, IEnumerable{Assembly}?, bool, ServiceLifetime )"/>
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services, params Assembly[] assemblies) =>
        AddExceptionHandlerClasses(services, assemblies);

    /// <summary>
    /// Add services required for exception handling.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies">Assemblies containing exception handler classes.</param>
    /// <param name="includeDefaultHandlers">Specifies whether to use default error handlers.</param>
    /// <param name="serviceLifetime">Specifies the lifetime of exception handler services in an <see cref="ServiceCollection"/>.</param>
    /// <returns></returns>
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services, IEnumerable<Assembly>? assemblies, bool includeDefaultHandlers = true, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) =>
        AddExceptionHandlerClasses(services, assemblies, includeDefaultHandlers, serviceLifetime);

    private static IServiceCollection AddExceptionHandlerClasses(IServiceCollection services, IEnumerable<Assembly>? assembliesToScan, bool includeDefaultHandlers = true, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        var handlerInterfaceType = typeof(IExceptionHandler<>);
        var assembliesToScanList = (assembliesToScan ?? Enumerable.Empty<Assembly>()).ToList();

        if (includeDefaultHandlers)
            assembliesToScanList.Add(handlerInterfaceType.Assembly);

        var allTypes = assembliesToScanList
            .Where(x => !x.IsDynamic)
            .Distinct()
            .SelectMany(x => x.DefinedTypes)
            .Where(x => x.IsClass && !x.IsAbstract && x.IsAssignableToGenericType(handlerInterfaceType))
            .ToList();

        foreach (var typeInfo in allTypes)
        {
            var serviceType = typeInfo.ImplementedInterfaces.FirstOrDefault(x => x.GetGenericTypeDefinition() == handlerInterfaceType);
            if (serviceType is not null)
                services.Add(new ServiceDescriptor(serviceType, typeInfo, serviceLifetime));
        }

        return services;
    }
}