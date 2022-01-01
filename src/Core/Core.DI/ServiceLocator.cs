namespace Core.DI;

public static class ServiceLocator
{
    private static IServiceProvider? _serviceProvider;

    public static void Configure(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static IServiceProvider GetServiceProvider() =>
        _serviceProvider ?? throw new InvalidOperationException("ServiceProvider is not configured. Use the ServiceLocator.Configure() method in your application's startup file.");
}