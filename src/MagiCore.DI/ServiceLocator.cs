using Microsoft.Extensions.DependencyInjection;

namespace MagiCore.DI;

public static class ServiceLocator
{
    private static IServiceProvider? _serviceProvider;

    public static void Configure(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static IServiceProvider GetServiceProvider() =>
        _serviceProvider ?? throw new InvalidOperationException("ServiceProvider is not configured. Use the ServiceLocator.Configure() method in your application's startup file.");

    /// <inheritdoc cref="IServiceProvider.GetService" />
    public static object? GetService(Type serviceType) =>
        GetServiceProvider().GetService(serviceType);

    /// <inheritdoc cref="ServiceProviderServiceExtensions.GetService{T}" />
    public static T? GetService<T>() =>
         GetServiceProvider().GetService<T>();

    /// <inheritdoc cref="ServiceProviderServiceExtensions.GetRequiredService" />
    public static object GetRequiredService(Type serviceType) =>
        GetServiceProvider().GetRequiredService(serviceType);

    /// <inheritdoc cref="ServiceProviderServiceExtensions.GetRequiredService{T}" />
    public static T GetRequiredService<T>() where T : notnull =>
        GetServiceProvider().GetRequiredService<T>();
}
