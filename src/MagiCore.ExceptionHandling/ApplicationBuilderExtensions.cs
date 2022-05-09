using Microsoft.AspNetCore.Builder;

namespace MagiCore.ExceptionHandling;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds a middleware to the pipeline that will catch exceptions.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseMagiCoreExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        return app;
    }

}