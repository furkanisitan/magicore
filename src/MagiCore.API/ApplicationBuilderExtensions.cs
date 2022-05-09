using MagiCore.IoC;
using Microsoft.AspNetCore.Builder;

namespace MagiCore.API;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configures the <see cref="ServiceLocator"/> class.
    /// <seealso cref="ServiceLocator.Configure"/>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseServiceLocator(this IApplicationBuilder app)
    {
        ServiceLocator.Configure(app.ApplicationServices);
        return app;
    }
}