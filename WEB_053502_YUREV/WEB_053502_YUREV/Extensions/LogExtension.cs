using WEB_053502_YUREV.Middleware;

namespace WEB_053502_YUREV.Extensions;

public static class LogExtension
{
    public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LogMiddleware>();
    }
}