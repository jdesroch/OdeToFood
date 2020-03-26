using Microsoft.AspNetCore.Builder;

namespace OdeToFood.Middleware
{
    public static class SayHelloMiddlewareExtension
    {
        public static IApplicationBuilder UseSayHello(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SayHelloMiddleware>();
        }
    }
}
